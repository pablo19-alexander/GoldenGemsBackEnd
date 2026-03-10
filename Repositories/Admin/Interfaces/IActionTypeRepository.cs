using GoldenGemsBackEnd.Models.Security;

namespace GoldenGemsBackEnd.Repositories.Admin.Interfaces;

/// <summary>
/// Interfaz del repositorio para gestionar los tipos de acción.
/// </summary>
public interface IActionTypeRepository : IRepository<ActionType>
{

    /// <summary>
    /// Obtiene un tipo de acción por su código.
    /// </summary>
    Task<ActionType?> GetByCodeAsync(string code, CancellationToken cancellationToken);


    /// <summary>
    /// Verifica si existe un tipo de acción con el código especificado.
    /// </summary>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
}
