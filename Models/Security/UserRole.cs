using GoldenGemsBackEnd.Models;

namespace GoldenGemsBackEnd.Models.Security;

/// <summary>
/// Tabla de relación entre Usuarios y Roles.
/// Determina qué roles tiene asignado cada usuario.
/// </summary>
public class UserRole : BaseEntity
{
    /// <summary>
    /// ID del Usuario.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Referencia a la entidad Usuario.
    /// </summary>
    public User? User { get; set; }

    /// <summary>
    /// ID del Rol.
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// Referencia a la entidad Rol.
    /// </summary>
    public Role? Role { get; set; }
}
