using GoldenGemsBackEnd.Models.Security;

namespace GoldenGemsBackEnd.Repositories.Admin.Interfaces;

/// <summary>
/// Interfaz del repositorio para gestionar los tipos de acción.
/// </summary>
public interface IActionTypeRepository
{
    /// <summary>
    /// Obtiene todos los tipos de acción disponibles.
    /// </summary>
    Task<List<ActionType>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un tipo de acción por su identificador.
    /// </summary>
    Task<ActionType?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un tipo de acción por su código.
    /// </summary>
    Task<ActionType?> GetByCodeAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe un tipo de acción con el identificador especificado.
    /// </summary>
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe un tipo de acción con el código especificado.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
}
