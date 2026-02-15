using System.ComponentModel.DataAnnotations;

namespace GoldenGemsBackEnd.DTOs.Auth
{
    /// <summary>
    /// Datos requeridos para autenticar un usuario.
    /// </summary>
    public class LoginRequestDto
    {
        [Required]
        public string Identifier { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
