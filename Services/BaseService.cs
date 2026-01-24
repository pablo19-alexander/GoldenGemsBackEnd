namespace GoldenGemsBackEnd.Services
{
    /// <summary>
    /// Interfaz base para todos los servicios
    /// </summary>
    public interface IBaseService
    {
        // Métodos comunes para todos los servicios
    }

    /// <summary>
    /// Clase base abstracta para implementar servicios
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        protected readonly ILogger _logger;

        protected BaseService(ILogger logger)
        {
            _logger = logger;
        }
    }
}
