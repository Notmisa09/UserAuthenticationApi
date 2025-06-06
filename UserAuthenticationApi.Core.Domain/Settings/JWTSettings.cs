﻿namespace UserAuthenticationApi.Core.Domain.Settings
{
    public class JWTSettings
    {
        public string Token { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
    }
}
