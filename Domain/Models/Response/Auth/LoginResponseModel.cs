﻿namespace Domain.Models.Response.Auth
{
    public class LoginResponseModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
