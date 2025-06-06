namespace UserAuthenticationApi.Core.Application.Dtos
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public bool IsExpired => DateTime.UtcNow >= DateTime.UtcNow;
        public DateTime Expires { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
