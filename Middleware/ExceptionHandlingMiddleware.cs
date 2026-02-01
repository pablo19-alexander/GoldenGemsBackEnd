using System.Net;
using System.Text.Json;
using GoldenGemsBackEnd.DTOs;

namespace GoldenGemsBackEnd.Middleware
{
    /// <summary>
    /// Middleware para manejar excepciones no controladas en toda la aplicación.
    /// </summary>
    /// <remarks>
    /// Este middleware intercepta todas las excepciones que ocurran durante el procesamiento
    /// de las solicitudes HTTP y las convierte en respuestas JSON consistentes.
    /// 
    /// Funcionalidad:
    /// - Captura todas las excepciones no controladas
    /// - Registra el error mediante logging
    /// - Devuelve una respuesta JSON standardizada con el error
    /// - Establece el código de estado HTTP apropiado
    /// </remarks>
    public class ExceptionHandlingMiddleware
    {
        /// <summary>
        /// Delegado para procesar la siguiente solicitud en el pipeline.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Logger para registrar las excepciones que ocurran.
        /// </summary>
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Constructor del middleware de manejo de excepciones.
        /// </summary>
        /// <param name="next">El siguiente delegado en el pipeline de middlewares</param>
        /// <param name="logger">Logger inyectado para registrar excepciones</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoca el middleware y maneja cualquier excepción que ocurra.
        /// </summary>
        /// <param name="context">El contexto HTTP de la solicitud actual</param>
        /// <returns>Una tarea que representa la ejecución del middleware</returns>
        /// <remarks>
        /// Este método envuelve la ejecución de los próximos middlewares en un try-catch
        /// para capturar cualquier excepción no controlada que ocurra durante el procesamiento.
        /// </remarks>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Maneja la excepción capturada y genera una respuesta JSON estandarizada.
        /// </summary>
        /// <param name="context">El contexto HTTP actual</param>
        /// <param name="exception">La excepción capturada a ser procesada</param>
        /// <returns>Una tarea que representa la escritura de la respuesta</returns>
        /// <remarks>
        /// Esta función:
        /// - Establece el tipo de contenido a application/json
        /// - Establece el código de estado a 500 (Internal Server Error)
        /// - Crea una respuesta de error usando ApiResponse
        /// - Serializa la respuesta en formato JSON con nomenclatura camelCase
        /// </remarks>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = ApiResponse<object>.ErrorResponse(
                "An error occurred while processing your request",
                new List<string> { exception.Message }
            );

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}
