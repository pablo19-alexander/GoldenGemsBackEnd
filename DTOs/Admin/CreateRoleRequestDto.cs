using System.ComponentModel.DataAnnotations;

namespace GoldenGemsBackEnd.DTOs.Admin;

/// <summary>
/// DTO para solicitud de creación de un nuevo rol
/// </summary>
public class CreateRoleRequestDto
{
    /// <summary>
    /// Nombre del rol (requerido, único)
    /// </summary>
    [Required(ErrorMessage = "El nombre del rol es requerido")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del rol (opcional)
    /// </summary>
    [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
    public string? Description { get; set; }
}
