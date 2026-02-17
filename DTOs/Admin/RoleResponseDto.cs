namespace GoldenGemsBackEnd.DTOs.Admin;

/// <summary>
/// DTO para respuesta de un rol
/// </summary>
public class RoleResponseDto
{
    /// <summary>
    /// Identificador único del rol
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nombre del rol
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del rol
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Indica si el rol está activo
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
