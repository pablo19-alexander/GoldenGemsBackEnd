using GoldenGemsBackEnd.Models;
using GoldenGemsBackEnd.Models.People;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using GoldenGemsBackEnd.Models.Security;

/// <summary>
/// Entidad Usuario del sistema.
/// </summary>
[Comment("Tabla de Usuarios del sistema. Contiene credenciales y información de acceso.")]
public class User : BaseEntity
{
    /// <summary>
    /// Email único del usuario.
    /// </summary>
    [Comment("Email único del usuario.")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Nombre de usuario (username) único del sistema.
    /// </summary>
    [Comment("Nombre de usuario único para login en el sistema.")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña hasheada del usuario.
    /// </summary>
    [Comment("Contraseña hasheada del usuario.")]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Relación con los datos personales del usuario.
    /// </summary>
    [Comment("Relación con los datos personales del usuario.")]
    public Person? Person { get; set; }

    /// <summary>
    /// Relación con los roles del usuario.
    /// Un usuario puede tener múltiples roles.
    /// </summary>
    [Comment("Relación con los roles del usuario. Un usuario puede tener múltiples roles.")]
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
