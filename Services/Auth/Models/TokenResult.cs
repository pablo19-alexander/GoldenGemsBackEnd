namespace GoldenGemsBackEnd.Services.Auth.Models
{
    public record TokenResult(string Token, DateTime ExpiresAt);
}
