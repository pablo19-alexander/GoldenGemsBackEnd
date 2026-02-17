using System.ComponentModel.DataAnnotations;

namespace GoldenGemsBackEnd.DTOs.Auth
{
    /// <summary>
    /// Datos necesarios para registrar un nuevo usuario en el sistema.
    /// Los nuevos usuarios siempre reciben el rol "User" por defecto.
    /// Los usuarios se crean activos por defecto.
    /// </summary>
    public class RegisterRequestDto
    {
        /// <summary>
        /// Email del usuario (requerido, único)
        /// </summary>
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El email debe tener un formato válido")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Usuario/nombre de usuario (requerido, único, 3+ caracteres)
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [MinLength(3, ErrorMessage = "El usuario debe tener al menos 3 caracteres")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña (requerido, mínimo 8 caracteres, debe contener mayúsculas, minúsculas, números y caracteres especiales)
        /// </summary>
        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Primer nombre (requerido)
        /// </summary>
        [Required(ErrorMessage = "El primer nombre es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El primer nombre debe tener entre 2 y 100 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Segundo nombre (opcional)
        /// </summary>
        [StringLength(100, ErrorMessage = "El segundo nombre no puede exceder 100 caracteres")]
        public string? SecondName { get; set; }

        /// <summary>
        /// Primer apellido (requerido)
        /// </summary>
        [Required(ErrorMessage = "El primer apellido es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El primer apellido debe tener entre 2 y 100 caracteres")]
        public string FirstLastName { get; set; } = string.Empty;

        /// <summary>
        /// Segundo apellido (opcional)
        /// </summary>
        [StringLength(100, ErrorMessage = "El segundo apellido no puede exceder 100 caracteres")]
        public string? SecondLastName { get; set; }

        /// <summary>
        /// ID del tipo de documento (requerido, debe existir en la BD)
        /// </summary>
        [Required(ErrorMessage = "El tipo de documento es requerido")]
        public Guid DocumentTypeId { get; set; }

        /// <summary>
        /// Número de documento (requerido, único por tipo de documento)
        /// </summary>
        [Required(ErrorMessage = "El número de documento es requerido")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "El número de documento debe tener entre 5 y 50 caracteres")]
        public string DocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Lista opcional de roles que se deben asociar al usuario.
        /// Si no se proporciona, se asigna automáticamente el rol "User".
        /// </summary>
        public List<Guid> RoleIds { get; set; } = new();
    }
}
