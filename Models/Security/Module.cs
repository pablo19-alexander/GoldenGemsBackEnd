using GoldenGemsBackEnd.Models;

namespace GoldenGemsBackEnd.Models.Security;

/// <summary>
/// Entidad Módulo - Define los módulos del sistema.
/// </summary>
public class Module : BaseEntity
{
    /// <summary>
    /// Código único del módulo para referencias en código (ej: M0001).
    /// </summary>
    public string Code { get; set; } = string.Empty;
 
    /// <summary>
    /// Nombre del módulo (ej: Ventas, Inventario, Usuarios).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del módulo.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Ícono o identificador visual del módulo.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Orden de visualización del módulo en el menú.
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Relación con los formularios que pertenecen a este módulo.
    /// Un módulo puede tener múltiples formularios.
    /// </summary>
    public ICollection<Form> Forms { get; set; } = new List<Form>();

    /// <summary>
    /// Relación con las acciones que pertenecen a este módulo.
    /// Un módulo puede tener múltiples acciones.
    /// </summary>
    public ICollection<Actions> Actions { get; set; } = new List<Actions>();
}
