namespace GoldenGemsBackEnd.Configurations
{
    /// <summary>
    /// Settings used to generate and validate JWT access tokens.
    /// </summary>
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; }
    }
}
