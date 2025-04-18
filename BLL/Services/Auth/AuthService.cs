﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Models.Configuration;
using Domain.Models.DBModels;
using Domain.Models.Exceptions;
using Domain.Models.Identity;
using Domain.Models.Request.Auth;
using Domain.Models.Response;
using Domain.Models.Response.Auth;
using Domain.Models.SuccessCodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BLL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly UserManager<ApplicationUserDBModel> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        
        public AuthService(IOptions<JwtConfiguration> jwtConfiguration, UserManager<ApplicationUserDBModel> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<JsonResult> LoginAsync(LoginRequestModel request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username!) ?? await _userManager.FindByEmailAsync(request.Username!);

                if (user is null)
                {
                    return new JsonResult(new GeneralApiResponseModel()
                    {
                        ReturnCode = AppErrors.Auth.UserNotFound,
                        Message = "User name not found"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                if (!await _userManager.CheckPasswordAsync(user, request.Password!))
                {
                    return new JsonResult(new GeneralApiResponseModel
                    {
                        ReturnCode = AppErrors.Auth.PasswordIncorrect,
                        Message = "Password Incorrect"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                if (user.Provider != AuthConsts.LoginProviders.Password)
                {
                    return new JsonResult(new GeneralApiResponseModel
                    {
                        ReturnCode = AppErrors.Auth.ProviderMismatch,
                        Message = "Provider Mismatch"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };

                }

                var token = await CreateToken(user);
                var refreshToken = GenerateRefreshToken();
                user.RefreshToken = refreshToken;

                user.RefreshTokenExpiryTime = request.RememberMe ? DateTimeOffset.Now.AddHours(_jwtConfiguration.RememberMeRefreshTokenLifetimeHours)
                                                                         : DateTimeOffset.Now.AddHours(_jwtConfiguration.DefaultRefreshTokenLifetimeHours);

                await _userManager.UpdateAsync(user);

                return new JsonResult(new LoginResponseModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new GeneralApiResponseModel
                {
                    ReturnCode = AppErrors.General.InternalServerError,
                    Message = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        public async Task<JsonResult> RegisterAsync(RegisterRequestModel request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Username) || request.Username.Length < AuthConsts.UsernameMinLength)
            {
                errors.Add(AuthConsts.UsernameLengthValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.Email) || !Regex.IsMatch(request.Email, AuthConsts.EmailRegex))
            {
                errors.Add(AuthConsts.EmailValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.Password) || !Regex.IsMatch(request.Password, AuthConsts.PasswordRegex))
            {
                errors.Add(AuthConsts.PasswordValidationError);
            }

            if (errors.Any())
            {
                return new JsonResult(new GeneralApiResponseModel
                {
                    ReturnCode = string.Join("|", errors),
                    Message = "Register error"
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            try
            {
                var userByEmail = await _userManager.FindByEmailAsync(request.Email!);
                if (userByEmail is not null)
                {
                    return new JsonResult(new GeneralApiResponseModel
                    {
                        ReturnCode = AppErrors.Auth.EmailExists,
                        Message = "Email already exists"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                var userByUserName = await _userManager.FindByNameAsync(request.Username!);

                if (userByUserName is not null)
                {
                    return new JsonResult(new GeneralApiResponseModel
                    {
                        ReturnCode = AppErrors.Auth.UserNameExists,
                        Message = "User Name already exits"
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                var user = new ApplicationUserDBModel
                {
                    DateCreated = DateTime.UtcNow,
                    Email = request.Email,
                    UserName = request.Username,
                    Provider = AuthConsts.LoginProviders.Password,
                };

                var result = await _userManager.CreateAsync(user, request.Password!);
                if (!await _roleManager.RoleExistsAsync(Role.User))
                {
                    await _roleManager.CreateAsync(new IdentityRole<int> { Name = Role.User });
                }

                await _userManager.AddToRoleAsync(user, Role.User);

                if (!result.Succeeded)
                {
                    return new JsonResult(new GeneralApiResponseModel
                    {
                        ReturnCode = AppErrors.Auth.AddToRoleFailed,
                        Message = "Adding to roles failed "
                    })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                return new JsonResult(new GeneralApiResponseModel
                {
                    ReturnCode = AppSuccessCodes.CreateSuccess,
                    Message = "Registrated successfully"
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch
            {
                return new JsonResult(new GeneralApiResponseModel
                {
                    ReturnCode = AppErrors.General.InternalServerError,
                    Message = "Internal Server Error"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        public async Task<JsonResult> RefreshTokenAsync(RefreshTokenResponseModel request)
        {
            try
            {
                var accessToken = request.AccessToken;
                var refreshToken = request.RefreshToken;

                var principal = GetPrincipalFromExpiredToken(accessToken);
                if (principal == null)
                {
                    return new JsonResult(new GeneralApiResponseModel
                    {
                        ReturnCode = AppErrors.Auth.RefreshInvalidToken,
                        Message = "Invalid Token"
                    })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }

                var username = principal.Identity!.Name!;

                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return new JsonResult(new GeneralApiResponseModel
                    {
                        ReturnCode = AppErrors.Auth.UserNotFound,
                        Message = "User Not Found"
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                if (user.RefreshToken != request.RefreshToken)
                {
                    return new JsonResult(new GeneralApiResponseModel
                    {
                        ReturnCode = AppErrors.Auth.RefreshTokenMismatch,
                        Message = "Refresh Token Mismatch"
                    })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
                if (user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    return new JsonResult(new GeneralApiResponseModel
                    {
                        ReturnCode = AppErrors.Auth.RefreshTokenExpired,
                        Message = "Refresh Token Expired"
                    })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
                var newAccessToken = await CreateToken(user);
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);

                return new JsonResult(new RefreshTokenResponseModel
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                    RefreshToken = newRefreshToken
                })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new GeneralApiResponseModel
                {
                    ReturnCode = AppErrors.General.InternalServerError,
                    Message = ex.Message
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        public async Task<OperationResultModel<bool>> UpdateUserRolesAsync(UpdateUserRolesRequestModel request)
        {

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return OperationResultModel<bool>.Failure("User not found");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            foreach (var role in request.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {

                    return OperationResultModel<bool>.Failure($"Role '{role}' does not exist");
                }

                await _userManager.AddToRoleAsync(user, role);
            }

            return OperationResultModel<bool>.Success();
        }


        public async Task<OperationResultModel<bool>> CreateRoleAsync(string roleName)

        {
            if (string.IsNullOrEmpty(roleName))
            {
                return OperationResultModel<bool>.Failure("Role name is required");
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return OperationResultModel<bool>.Failure("Role already exists");
            }

            var result = await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
            if (!result.Succeeded)
            {
                return OperationResultModel<bool>.Failure($"Create error: {result.Errors.FirstOrDefault()?.ToString()}");
            }

            return OperationResultModel<bool>.Success();
        }


        public async Task<List<string>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles
                .Select(r => r.Name)
                .ToListAsync();

            return roles ?? new List<string>();
        }


        private async Task<JwtSecurityToken> CreateToken(ApplicationUserDBModel user)
        {
                var authClaims = await GetClaims(user);
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key!));

                var token = new JwtSecurityToken(
                    issuer: _jwtConfiguration.Issuer,
                    audience: _jwtConfiguration.Audience,
                    expires: DateTime.Now.AddHours(_jwtConfiguration.AccessTokenLifetimeMin),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                return token;
        }


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        private async Task<List<Claim>> GetClaims(ApplicationUserDBModel user)
        {
            var authClaims = new List<Claim>
        {
            //new(ClaimTypes.Sid, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new("registration_date", user.DateCreated.ToString("O")),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Any())
            {
                authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
            }

            return authClaims;
        }


        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key!)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException(AppErrors.Auth.RefreshInvalidToken);

            return principal;
        }

    }
}
