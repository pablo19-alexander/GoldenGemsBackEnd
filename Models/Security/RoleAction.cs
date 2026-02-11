using GoldenGemsBackEnd.Models;

namespace GoldenGemsBackEnd.Models.Security;

/// <summary>
/// Tabla de relación entre Roles y Acciones.
/// Define qué acciones puede realizar cada rol.
/// </summary>
public class RoleAction : BaseEntity
{
    /// <summary>
    /// ID del Rol.
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Referencia a la entidad Rol.
    /// </summary>
    public Role? Role { get; set; }

    /// <summary>
    /// ID de la Acción.
    /// </summary>
    public Guid ActionId { get; set; }

    /// <summary>
    /// Referencia a la entidad Acción.
    /// </summary>
    public Actions? Action { get; set; }
}
