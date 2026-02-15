using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Services.Auth.Models;

namespace GoldenGemsBackEnd.Services.Auth.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Emite un token firmando las claims del usuario y roles autorizados.
        /// </summary>
        TokenResult GenerateToken(User user, IEnumerable<string> roles);
    }
}
