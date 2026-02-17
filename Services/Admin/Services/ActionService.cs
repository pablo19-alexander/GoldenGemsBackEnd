using GoldenGemsBackEnd.DTOs;
using GoldenGemsBackEnd.DTOs.Admin;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Admin.Interfaces;
using GoldenGemsBackEnd.Services.Admin.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoldenGemsBackEnd.Services.Admin.Services;

/// <summary>
/// Implementación del servicio para gestión de acciones
/// </summary>
public class ActionService : BaseService, IActionService
{
    private readonly IActionRepository _actionRepository;

    public ActionService(IActionRepository actionRepository, ILogger<ActionService> logger)
        : base(logger)
    {
        _actionRepository = actionRepository ?? throw new ArgumentNullException(nameof(actionRepository));
    }

    /// <summary>
    /// Crea una nueva acción en el sistema
    /// </summary>
    public async Task<ApiResponse<ActionResponseDto>> CreateActionAsync(CreateActionRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            // Validar solicitud
            if (request == null)
                return ApiResponse<ActionResponseDto>.ErrorResponse("La solicitud es nula");

            if (string.IsNullOrWhiteSpace(request.Code))
                return ApiResponse<ActionResponseDto>.ErrorResponse("El código de la acción es requerido");

            if (string.IsNullOrWhiteSpace(request.Name))
                return ApiResponse<ActionResponseDto>.ErrorResponse("El nombre de la acción es requerido");

            var code = request.Code.Trim().ToUpper();
            var name = request.Name.Trim();

            // Validar código único
            if (await _actionRepository.ExistsByCodeAsync(code, cancellationToken))
            {
                _logger.LogWarning($"Intento de crear acción con código duplicado: {code}");
                return ApiResponse<ActionResponseDto>.ErrorResponse($"Ya existe una acción con el código '{code}'");
            }

            // Crear acción
            var action = new Actions
            {
                Id = Guid.NewGuid(),
                Name = name,
                Code = code,
                Description = request.Description?.Trim(),
                ActionType = request.ActionType,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            // Guardar en BD
            var createdAction = await _actionRepository.CreateAsync(action, cancellationToken);

            // Mapear a DTO de respuesta
            var actionDto = MapActionToDto(createdAction);

            _logger.LogInformation($"Acción creada exitosamente: {action.Code} (ID: {action.Id})");

            return ApiResponse<ActionResponseDto>.SuccessResponse(actionDto, "Acción creada exitosamente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear acción");
            return ApiResponse<ActionResponseDto>.ErrorResponse("Error al crear la acción");
        }
    }

    /// <summary>
    /// Obtiene todas las acciones del sistema
    /// </summary>
    public async Task<ApiResponse<List<ActionResponseDto>>> GetAllActionsAsync(CancellationToken cancellationToken)
    {
        try
        {
            var actions = await _actionRepository.GetAllAsync(cancellationToken);
            var actionDtos = actions.Select(MapActionToDto).ToList();

            return ApiResponse<List<ActionResponseDto>>.SuccessResponse(
                actionDtos,
                $"Se encontraron {actionDtos.Count} acciones"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todas las acciones");
            return ApiResponse<List<ActionResponseDto>>.ErrorResponse("Error al obtener las acciones");
        }
    }

    /// <summary>
    /// Verifica si existe una acción con el código especificado
    /// </summary>
    public async Task<bool> ActionExistsByCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            return false;

        return await _actionRepository.ExistsByCodeAsync(code, cancellationToken);
    }

    /// <summary>
    /// Obtiene una acción por su ID
    /// </summary>
    public async Task<Actions?> GetActionByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            return null;

        return await _actionRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <summary>
    /// Obtiene una acción por su código
    /// </summary>
    public async Task<Actions?> GetActionByCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            return null;

        return await _actionRepository.GetByCodeAsync(code, cancellationToken);
    }

    /// <summary>
    /// Mapea una entidad Actions a su DTO de respuesta
    /// </summary>
    private static ActionResponseDto MapActionToDto(Actions action)
    {
        return new ActionResponseDto
        {
            Id = action.Id,
            Name = action.Name,
            Code = action.Code,
            Description = action.Description,
            ActionType = action.ActionType,
            IsActive = action.IsActive,
            CreatedAt = action.CreatedAt
        };
    }
}
