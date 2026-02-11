using GoldenGemsBackEnd.Models;

namespace GoldenGemsBackEnd.Models.Security;

/// <summary>
/// Entidad Rol - Define los roles disponibles en el sistema.
/// </summary>
public class Role : BaseEntity
{
    /// <summary>
    /// Nombre único del rol (ej: Admin, User, Viewer).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del rol y sus responsabilidades.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Relación con las acciones que permite este rol.
    /// Un rol puede tener múltiples acciones.
    /// </summary>
    public ICollection<RoleAction> RoleActions { get; set; } = new List<RoleAction>();

    /// <summary>
    /// Relación con los usuarios que tienen este rol.
    /// Un rol puede ser asignado a múltiples usuarios.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
