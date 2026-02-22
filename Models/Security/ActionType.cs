using GoldenGemsBackEnd.Models;

namespace GoldenGemsBackEnd.Models.Security;

/// <summary>
/// Catálogo de tipos de acción disponibles en el sistema.
/// </summary>
public class ActionType : BaseEntity
{
    /// <summary>
    /// Código único del tipo de acción (ej: 001, 002, 003).
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del tipo de acción.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Acciones asociadas a este tipo.
    /// </summary>
    public ICollection<Actions> Actions { get; set; } = new List<Actions>();
}
