using GoldenGemsBackEnd.DTOs;
using GoldenGemsBackEnd.DTOs.Admin;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Services;

namespace GoldenGemsBackEnd.Services.Admin.Interfaces;

/// <summary>
/// Interfaz del servicio para gestión de roles
/// </summary>
public interface IRoleService : IBaseService
{
    /// <summary>
    /// Crea un nuevo rol en el sistema
    /// </summary>
    /// <param name="request">Datos del rol a crear</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Respuesta con el rol creado o errores de validación</returns>
    Task<ApiResponse<RoleResponseDto>> CreateRoleAsync(CreateRoleRequestDto request, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene todos los roles del sistema
    /// </summary>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Respuesta con lista de roles</returns>
    Task<ApiResponse<List<RoleResponseDto>>> GetAllRolesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Verifica si existe un rol con el nombre especificado
    /// </summary>
    /// <param name="name">Nombre del rol a verificar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el rol existe, false en caso contrario</returns>
    Task<bool> RoleExistsByNameAsync(string name, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un rol por su ID
    /// </summary>
    /// <param name="id">ID del rol</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El rol si existe, null en caso contrario</returns>
    Task<Role?> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene un rol por su nombre
    /// </summary>
    /// <param name="name">Nombre del rol</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>El rol si existe, null en caso contrario</returns>
    Task<Role?> GetRoleByNameAsync(string name, CancellationToken cancellationToken);
}
