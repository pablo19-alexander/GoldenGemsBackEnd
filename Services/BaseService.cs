namespace GoldenGemsBackEnd.Services
{
    /// <summary>
    /// Interfaz base que define el contrato mínimo para todos los servicios de la aplicación.
    /// </summary>
    /// <remarks>
    /// Esta interfaz actúa como marcador para identificar servicios dentro de la aplicación.
    /// Los servicios específicos deben heredar de esta interfaz y extender su funcionalidad.
    /// </remarks>
    public interface IBaseService
    {
        // Los métodos comunes serán agregados según necesidades
    }

    /// <summary>
    /// Clase base abstracta que proporciona funcionalidad común a todos los servicios.
    /// </summary>
    /// <remarks>
    /// Esta clase base:
    /// - Proporciona acceso a un logger para registrar eventos y errores
    /// - Define la estructura común que deben seguir todos los servicios
    /// - Permite la inyección de dependencias del logger
    /// 
    /// Los servicios concretos deben heredar de esta clase para reutilizar la funcionalidad de logging.
    /// </remarks>
    public abstract class BaseService : IBaseService
    {
        /// <summary>
        /// Logger compartido para todos los servicios que heredan de esta clase.
        /// Utilizado para registrar información de depuración, advertencias y errores.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Constructor de la clase base del servicio.
        /// </summary>
        /// <param name="logger">Instancia del logger inyectada por dependencia</param>
        /// <remarks>
        /// Inicializa el logger que será utilizado por los servicios derivados
        /// para registrar eventos durante su ejecución.
        /// </remarks>
        protected BaseService(ILogger logger)
        {
            _logger = logger;
        }
    }
}
