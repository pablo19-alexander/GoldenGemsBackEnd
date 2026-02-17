using GoldenGemsBackEnd.Models.Security;

namespace GoldenGemsBackEnd.Repositories.Auth.Interfaces;

/// <summary>
/// Interfaz del repositorio para la entidad User
/// Define operaciones específicas para gestionar usuarios
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Crea un nuevo usuario en la base de datos
    /// </summary>
    /// <param name="user">Objeto User a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El usuario creado con su ID asignado</returns>
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un usuario por su email
    /// </summary>
    /// <param name="email">Email del usuario a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El usuario si existe, null en caso contrario</returns>
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un usuario por su username
    /// </summary>
    /// <param name="username">Username del usuario a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El usuario si existe, null en caso contrario</returns>
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un usuario por su ID incluyendo sus roles relacionados
    /// </summary>
    /// <param name="id">ID del usuario</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El usuario con sus roles si existe, null en caso contrario</returns>
    Task<User?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe un usuario con el email especificado
    /// </summary>
    /// <param name="email">Email a verificar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el email existe, false en caso contrario</returns>
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe un usuario con el username especificado
    /// </summary>
    /// <param name="username">Username a verificar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el username existe, false en caso contrario</returns>
    Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Guarda los cambios en la base de datos
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Número de registros afectados</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
