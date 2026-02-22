using System.ComponentModel.DataAnnotations;

namespace GoldenGemsBackEnd.DTOs.Admin;

/// <summary>
/// DTO para solicitud de creación de una nueva acción
/// </summary>
public class CreateActionRequestDto
{
    /// <summary>
    /// Nombre de la acción (requerido, único)
    /// </summary>
    [Required(ErrorMessage = "El nombre de la acción es requerido")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Código único de la acción (requerido, único)
    /// Ejemplo: "USER_CREATE", "USER_READ", "USER_UPDATE"
    /// </summary>
    [Required(ErrorMessage = "El código de la acción es requerido")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El código debe tener entre 3 y 50 caracteres")]
    [RegularExpression(@"^[A-Z0-9_]+$", ErrorMessage = "El código solo puede contener mayúsculas, números y guiones bajos")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la acción (opcional)
    /// </summary>
    [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    public string? Description { get; set; }

    /// <summary>
    /// Identificador del tipo de acción (catálogo ActionType)
    /// </summary>
    [Required(ErrorMessage = "El tipo de acción es requerido")]
    public Guid ActionTypeId { get; set; }
}
