using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories;

namespace GoldenGemsBackEnd.Repositories.Admin.Interfaces;

/// <summary>
/// Interfaz del repositorio para la entidad Action
/// Define operaciones específicas para gestionar acciones
/// </summary>
public interface IActionRepository : IRepository<Actions>
{

    /// <summary>
    /// Verifica si existe una acción con el código especificado
    /// </summary>
    /// <param name="code">Código de la acción a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si la acción existe, false en caso contrario</returns>
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una acción por su código
    /// </summary>
    /// <param name="code">Código de la acción a buscar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La acción si existe, null en caso contrario</returns>
    Task<Actions?> GetByCodeAsync(string code, CancellationToken cancellationToken);

}
