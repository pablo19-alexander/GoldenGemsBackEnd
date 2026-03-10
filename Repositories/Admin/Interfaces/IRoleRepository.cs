using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories;

namespace GoldenGemsBackEnd.Repositories.Admin.Interfaces;

/// <summary>
/// Interfaz del repositorio para la entidad Role
/// Define operaciones específicas para gestionar roles
/// </summary>
public interface IRoleRepository : IRepository<Role>
{

    /// <summary>
    /// Verifica si existe un rol con el nombre especificado
    /// </summary>
    /// <param name="name">Nombre del rol a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el rol existe, false en caso contrario</returns>
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un rol por su nombre
    /// </summary>
    /// <param name="name">Nombre del rol a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El rol si existe, null en caso contrario</returns>
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);

}
