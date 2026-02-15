using GoldenGemsBackEnd.DTOs;
using GoldenGemsBackEnd.DTOs.Auth;

namespace GoldenGemsBackEnd.Services.Auth.Interfaces
{
    public interface IAuthService : IBaseService
    {
        /// <summary>
        /// Registra un usuario y retorna el token emitido en la misma operación.
        /// </summary>
        Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Autentica un usuario existente y devuelve la información básica junto al JWT.
        /// </summary>
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
    }
}
