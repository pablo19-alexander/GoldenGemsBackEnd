using GoldenGemsBackEnd.Models;

namespace GoldenGemsBackEnd.Repositories
{
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
        /// Obtiene una entidad por su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a buscar</param>
        /// <returns>La entidad encontrada o null si no existe</returns>
        Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        /// Obtiene todas las entidades del tipo especificado.
        /// </summary>
        /// <returns>Colección de todas las entidades</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Agrega una nueva entidad al repositorio.
        /// </summary>
        /// <param name="entity">La entidad a agregar</param>
        /// <returns>La entidad agregada con su ID generado</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Actualiza una entidad existente.
        /// </summary>
        /// <param name="entity">La entidad con los datos actualizados</param>
        /// <returns>La entidad actualizada</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Elimina una entidad por su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a eliminar</param>
        /// <returns>true si la eliminación fue exitosa, false en caso contrario</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Verifica si una entidad existe por su identificador único.
        /// </summary>
        /// <param name="id">El ID de la entidad a verificar</param>
        /// <returns>true si la entidad existe, false en caso contrario</returns>
        Task<bool> ExistsAsync(Guid id);
    }
}
