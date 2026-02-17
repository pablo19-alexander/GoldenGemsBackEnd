using GoldenGemsBackEnd.DTOs;
using GoldenGemsBackEnd.DTOs.Admin;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Services;

namespace GoldenGemsBackEnd.Services.Admin.Interfaces;

/// <summary>
/// Interfaz del servicio para gestión de acciones
/// </summary>
public interface IActionService : IBaseService
{
    /// <summary>
    /// Crea una nueva acción en el sistema
    /// </summary>
    /// <param name="request">Datos de la acción a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Respuesta con la acción creada o errores de validación</returns>
    Task<ApiResponse<ActionResponseDto>> CreateActionAsync(CreateActionRequestDto request, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todas las acciones del sistema
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Respuesta con lista de acciones</returns>
    Task<ApiResponse<List<ActionResponseDto>>> GetAllActionsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe una acción con el código especificado
    /// </summary>
    /// <param name="code">Código de la acción a verificar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si la acción existe, false en caso contrario</returns>
    Task<bool> ActionExistsByCodeAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una acción por su ID
    /// </summary>
    /// <param name="id">ID de la acción</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La acción si existe, null en caso contrario</returns>
    Task<Actions?> GetActionByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene una acción por su código
    /// </summary>
    /// <param name="code">Código de la acción</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>La acción si existe, null en caso contrario</returns>
    Task<Actions?> GetActionByCodeAsync(string code, CancellationToken cancellationToken);
}
