using GoldenGemsBackEnd.DTOs.Admin;
using GoldenGemsBackEnd.Services.Admin.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoldenGemsBackEnd.Controllers;

/// <summary>
/// Controlador para la gestión de acciones
/// Requiere autorización con rol "Admin"
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ActionController : ControllerBase
{
    private readonly IActionService _actionService;

    public ActionController(IActionService actionService)
    {
        _actionService = actionService ?? throw new ArgumentNullException(nameof(actionService));
    }

    /// <summary>
    /// Crea una nueva acción en el sistema
    /// Solo accesible por administradores
    /// </summary>
    /// <param name="request">Datos de la acción a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Respuesta con la acción creada</returns>
    [HttpPost("create")]
    public async Task<IActionResult> CreateAction([FromBody] CreateActionRequestDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _actionService.CreateActionAsync(request, cancellationToken);

        if (!result.Success)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetAllActions), result);
    }

    /// <summary>
    /// Obtiene todas las acciones del sistema
    /// Solo accesible por administradores
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de todas las acciones</returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllActions(CancellationToken cancellationToken)
    {
        var result = await _actionService.GetAllActionsAsync(cancellationToken);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}
