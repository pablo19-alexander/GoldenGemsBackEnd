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
    /// Identificador del tipo de acción asociado.
    /// </summary>
    public Guid ActionTypeId { get; set; }

    /// <summary>
    /// Código del tipo de acción.
    /// </summary>
    public string ActionTypeCode { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del tipo de acción.
    /// </summary>
    public string? ActionTypeDescription { get; set; }

    /// <summary>
    /// Indica si la acción está activa
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de creación
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
