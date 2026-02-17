using GoldenGemsBackEnd.Models.Security;

namespace GoldenGemsBackEnd.Repositories.Admin.Interfaces;

/// <summary>
/// Interfaz del repositorio para la entidad Role
/// Define operaciones específicas para gestionar roles
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Crea un nuevo rol en la base de datos
    /// </summary>
    /// <param name="role">Objeto Role a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El rol creado con su ID asignado</returns>
    Task<Role> CreateAsync(Role role, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todos los roles de la base de datos
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de todos los roles</returns>
    Task<List<Role>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todos los roles activos
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de roles activos</returns>
    Task<List<Role>> GetAllActiveAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe un rol con el nombre especificado
    /// </summary>
    /// <param name="name">Nombre del rol a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el rol existe, false en caso contrario</returns>
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un rol por su identificador único
    /// </summary>
    /// <param name="id">Identificador del rol</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El rol si existe, null en caso contrario</returns>
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un rol por su nombre
    /// </summary>
    /// <param name="name">Nombre del rol a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El rol si existe, null en caso contrario</returns>
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Guarda los cambios en la base de datos
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Número de registros afectados</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
