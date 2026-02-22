using GoldenGemsBackEnd.Models;

namespace GoldenGemsBackEnd.Models.Security;

/// <summary>
/// Entidad Acción - Define las acciones disponibles en módulos, formularios y procesos.
/// </summary>
public class Actions : BaseEntity
{
    /// <summary>
    /// Nombre de la acción (ej: Create, Read, Update, Delete, View, Approve).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción de la acción que realiza.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Código único de la acción para referencias en código (ej: ACT_CREATE_INVOICE).
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Identificador del tipo de acción asociado.
    /// </summary>
    public Guid ActionTypeId { get; set; }

    /// <summary>
    /// Referencia al tipo de acción asociado.
    /// </summary>
    public ActionType? ActionType { get; set; }

    /// <summary>
    /// ID del módulo al que pertenece la acción (opcional).
    /// </summary>
    public Guid? ModuleId { get; set; }

    /// <summary>
    /// ID del formulario al que pertenece la acción (opcional).
    /// </summary>
    public Guid? FormId { get; set; }

    /// <summary>
    /// Referencia a la entidad Módulo.
    /// </summary>
    public Module? Module { get; set; }

    /// <summary>
    /// Referencia a la entidad Formulario.
    /// </summary>
    public Form? Form { get; set; }

    /// <summary>
    /// Relación con los roles que pueden ejecutar esta acción.
    /// Una acción puede ser ejecutada por múltiples roles.
    /// </summary>
    public ICollection<RoleAction> RoleActions { get; set; } = new List<RoleAction>();
}
