using System.ComponentModel.DataAnnotations;

namespace GoldenGemsBackEnd.DTOs.Auth
{
    /// <summary>
    /// Datos necesarios para registrar un nuevo usuario en el sistema.
    /// </summary>
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        /// <summary>
        /// Lista opcional de roles que se deben asociar al usuario.
        /// </summary>
        public List<Guid> RoleIds { get; set; } = new();
    }
}
