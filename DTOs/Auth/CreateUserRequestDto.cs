using System.ComponentModel.DataAnnotations;

namespace GoldenGemsBackEnd.DTOs.Auth;

/// <summary>
/// DTO para solicitud de creación de usuario administrativo
/// Solo accesible por administradores
/// </summary>
public class CreateUserRequestDto
{
    /// <summary>
    /// Email del usuario (requerido, único)
    /// </summary>
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El email debe tener un formato válido")]
    [StringLength(255, ErrorMessage = "El email no puede exceder 255 caracteres")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Usuario/nombre de usuario (requerido, único, 3+ caracteres)
    /// </summary>
    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 100 caracteres")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña (requerido, mínimo 8 caracteres, debe contener mayúsculas, minúsculas, números y caracteres especiales)
    /// </summary>
    [Required(ErrorMessage = "La contraseña es requerida")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 255 caracteres")]
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
    /// IDs de los roles a asignar al usuario (requerido, mínimo 1 rol)
    /// </summary>
    [Required(ErrorMessage = "Al menos un rol es requerido")]
    public List<Guid> RoleIds { get; set; } = new();

    /// <summary>
    /// Indica si el usuario está activo (default: true)
    /// </summary>
    public bool IsActive { get; set; } = true;
}
