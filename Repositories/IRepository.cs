using GoldenGemsBackEnd.Models;
using System.Threading;

namespace GoldenGemsBackEnd.Repositories;

/// <summary>
/// Interfaz genérica que define el contrato para implementar el patrón Repository.
/// </summary>
/// <typeparam name="T">Tipo de entidad que hereda de BaseEntity</typeparam>
/// <remarks>
/// Esta interfaz proporciona operaciones CRUD básicas para cualquier entidad del dominio.
/// Implementa el patrón Repository para abstraer la lógica de acceso a datos.
/// </remarks>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Crea una nueva entidad en la fuente de datos y retorna la instancia persistida.
    /// </summary>
    /// <param name="entity">Entidad a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Actualiza una entidad existente y persiste los cambios inmediatamente.
    /// </summary>
    /// <param name="entity">Entidad a modificar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todas las entidades del tipo especificado.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de todas las entidades</returns>
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todas las entidades activas del tipo especificado.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de entidades activas</returns>
    Task<List<T>> GetAllActiveAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una entidad por su identificador único.
    /// </summary>
    /// <param name="id">Identificador de la entidad</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Entidad encontrada o null si no existe</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe una entidad con el identificador específico.
    /// </summary>
    /// <param name="id">Identificador de la entidad</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Verdadero si existe, Falso en caso contrario</returns>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Elimina lógicamente una entidad (Soft Delete) cambiando su estado IsActive a false.
    /// </summary>
    /// <param name="entity">Entidad a eliminar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    Task<T> DeleteAsync(T entity, CancellationToken cancellationToken);
}
