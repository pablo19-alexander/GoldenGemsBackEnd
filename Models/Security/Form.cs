using GoldenGemsBackEnd.Models;

namespace GoldenGemsBackEnd.Models.Security;

/// <summary>
/// Entidad Formulario - Define los formularios disponibles en los módulos.
/// </summary>
public class Form : BaseEntity
{
    /// <summary>
    /// Código único del formulario para referencias en código (ej: F00001).
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// refenerencia unica del formulario (ej: FrmUser)
    /// </summary>
    public string FormReference { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del formulario (ej: Crear Usuario).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del formulario.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Ruta o URL del formulario en el sistema.
    /// </summary>
    public string? Route { get; set; }

    /// <summary>
    /// ID del módulo al que pertenece este formulario.
    /// </summary>
    public Guid ModuleId { get; set; }

    /// <summary>
    /// Referencia a la entidad Módulo.
    /// </summary>
    public Module? Module { get; set; }

    /// <summary>
    /// Orden de visualización del formulario dentro del módulo.
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Relación con las acciones que pertenecen a este formulario.
    /// Un formulario puede tener múltiples acciones.
    /// </summary>
    public ICollection<Actions> Actions { get; set; } = new List<Actions>();
}
