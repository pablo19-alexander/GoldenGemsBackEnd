namespace GoldenGemsBackEnd.DTOs.Admin;

/// <summary>
/// DTO para respuesta de una acción
/// </summary>
public class ActionResponseDto
{
    /// <summary>
    /// Identificador único de la acción
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Nombre de la acción
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Código único de la acción
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la acción
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Tipo de acción (ej: Form, Process, View, etc.)
    /// </summary>
    public string ActionType { get; set; } = string.Empty;

    /// <summary>
    /// Indica si la acción está activa
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
