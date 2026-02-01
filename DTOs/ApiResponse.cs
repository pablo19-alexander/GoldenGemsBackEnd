namespace GoldenGemsBackEnd.DTOs
{
    /// <summary>
    /// Clase genérica que estandariza la estructura de respuestas de la API.
    /// </summary>
    /// <typeparam name="T">El tipo de datos contenido en la respuesta</typeparam>
    /// <remarks>
    /// Esta clase proporciona una estructura consistente para todas las respuestas de la API,
    /// facilitando el manejo de respuestas exitosas y errores de manera uniforme en los clientes.
    /// 
    /// Estructura:
    /// - Success: Indicador booleano del resultado de la operación
    /// - Message: Mensaje descriptivo de la operación
    /// - Data: Los datos retornados por la operación (si es exitosa)
    /// - Errors: Lista de mensajes de error (si hay errores)
    /// </remarks>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        /// <value>true si la operación fue exitosa, false en caso contrario</value>
        public bool Success { get; set; }

        /// <summary>
        /// Mensaje descriptivo sobre el resultado de la operación.
        /// </summary>
        /// <value>Texto que describe el resultado de la operación</value>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Datos retornados por la operación.
        /// </summary>
        /// <value>Los datos del tipo genérico T, o null si no hay datos</value>
        /// <remarks>
        /// Esta propiedad contendrá los datos cuando la operación es exitosa.
        /// Será null en casos de error.
        /// </remarks>
        public T? Data { get; set; }

        /// <summary>
        /// Lista de mensajes de error que ocurrieron durante la operación.
        /// </summary>
        /// <value>Colección de strings con mensajes de error, vacía si no hay errores</value>
        /// <remarks>
        /// Se utiliza para proporcionar detalles específicos sobre qué salió mal
        /// en caso de que la operación falle.
        /// </remarks>
        public List<string> Errors { get; set; } = new();

        /// <summary>
        /// Crea una respuesta exitosa con los datos especificados.
        /// </summary>
        /// <param name="data">Los datos a incluir en la respuesta</param>
        /// <param name="message">Mensaje descriptivo de la operación exitosa</param>
        /// <returns>Una instancia de ApiResponse configurada para una operación exitosa</returns>
        /// <remarks>
        /// Método helper para crear rápidamente respuestas exitosas.
        /// Establece Success en true y rellena los campos Data y Message.
        /// </remarks>
        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Crea una respuesta de error con el mensaje y lista de errores especificados.
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error</param>
        /// <param name="errors">Lista de errores específicos ocurridos</param>
        /// <returns>Una instancia de ApiResponse configurada para una operación fallida</returns>
        /// <remarks>
        /// Método helper para crear respuestas de error de manera consistente.
        /// Establece Success en false y rellena los campos Message y Errors.
        /// Si errors es null, se inicializa con una lista vacía.
        /// </remarks>
        public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
