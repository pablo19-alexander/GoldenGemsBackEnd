using GoldenGemsBackEnd.DTOs.Admin;
using GoldenGemsBackEnd.Services.Admin.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoldenGemsBackEnd.Controllers;

/// <summary>
/// Controlador para la gestión de roles
/// Requiere autorización con rol "Admin"
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
    }

    /// <summary>
    /// Crea un nuevo rol en el sistema
    /// Solo accesible por administradores
    /// </summary>
    /// <param name="request">Datos del rol a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Respuesta con el rol creado</returns>
    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequestDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _roleService.CreateRoleAsync(request, cancellationToken);

        if (!result.Success)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetAllRoles), result);
    }

    /// <summary>
    /// Obtiene todos los roles del sistema
    /// Solo accesible por administradores
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Lista de todos los roles</returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var result = await _roleService.GetAllRolesAsync(cancellationToken);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}
