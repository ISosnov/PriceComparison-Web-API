﻿namespace Domain.Models.Request.Auth
{
    public class RefreshTokenRequestModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
