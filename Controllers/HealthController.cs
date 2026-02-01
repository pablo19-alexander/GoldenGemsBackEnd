using Microsoft.AspNetCore.Mvc;

namespace GoldenGemsBackEnd.Controllers
{
    /// <summary>
    /// Controlador para verificar el estado de salud de la API.
    /// </summary>
    /// <remarks>
    /// Este controlador proporciona un endpoint para monitoreo y health checks.
    /// Útil para verificar que la API está en funcionamiento.
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Obtiene el estado de salud actual de la API.
        /// </summary>
        /// <returns>
        /// Retorna un objeto con información del estado de la API incluyendo:
        /// - status: Estado actual (healthy)
        /// - timestamp: Fecha y hora UTC actual
        /// - service: Nombre del servicio
        /// </returns>
        /// <remarks>
        /// Endpoint: GET /api/health
        /// Respuesta: 200 OK con objeto de estado
        /// </remarks>
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new
            {
                status = "healthy",
                timestamp = DateTime.UtcNow,
                service = "GoldenGems API"
            });
        }
    }
}
