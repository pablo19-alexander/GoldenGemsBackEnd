namespace GoldenGemsBackEnd.Models
{
    /// <summary>
    /// Clase base abstracta que proporciona propiedades comunes a todas las entidades del dominio.
    /// </summary>
    /// <remarks>
    /// Esta clase define los campos estándar que todas las entidades deben tener:
    /// - Id: Identificador único basado en GUID
    /// - CreatedAt: Fecha y hora de creación
    /// - UpdatedAt: Fecha y hora de última actualización (nullable)
    /// - IsActive: Indicador de si la entidad está activa (soft delete)
    /// </remarks>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identificador único de la entidad basado en GUID.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Fecha y hora UTC en que fue creada la entidad.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Fecha y hora UTC de la última actualización de la entidad.
        /// Puede ser null si la entidad aún no ha sido actualizada.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Indicador de estado de la entidad para implementar soft delete.
        /// Por defecto es true (activa).
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Constructor protegido que inicializa los valores por defecto de la entidad.
        /// </summary>
        /// <remarks>
        /// Inicializa:
        /// - CreatedAt con la hora UTC actual
        /// </remarks>
        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
