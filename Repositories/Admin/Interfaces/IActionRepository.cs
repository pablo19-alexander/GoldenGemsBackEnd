using GoldenGemsBackEnd.Models.Security;

namespace GoldenGemsBackEnd.Repositories.Admin.Interfaces;

/// <summary>
/// Interfaz del repositorio para la entidad Action
/// Define operaciones específicas para gestionar acciones
/// </summary>
public interface IActionRepository
{
    /// <summary>
    /// Crea una nueva acción en la base de datos
    /// </summary>
    /// <param name="action">Objeto Action a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La acción creada con su ID asignado</returns>
    Task<Actions> CreateAsync(Actions action, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todas las acciones de la base de datos
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de todas las acciones</returns>
    Task<List<Actions>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todas las acciones activas
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de acciones activas</returns>
    Task<List<Actions>> GetAllActiveAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe una acción con el código especificado
    /// </summary>
    /// <param name="code">Código de la acción a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si la acción existe, false en caso contrario</returns>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una acción por su identificador único
    /// </summary>
    /// <param name="id">Identificador de la acción</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La acción si existe, null en caso contrario</returns>
    Task<Actions?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una acción por su código
    /// </summary>
    /// <param name="code">Código de la acción a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La acción si existe, null en caso contrario</returns>
    Task<Actions?> GetByCodeAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Guarda los cambios en la base de datos
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Número de registros afectados</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
