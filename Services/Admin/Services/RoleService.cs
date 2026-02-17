using GoldenGemsBackEnd.DTOs;
using GoldenGemsBackEnd.DTOs.Admin;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Admin.Interfaces;
using GoldenGemsBackEnd.Services.Admin.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoldenGemsBackEnd.Services.Admin.Services;

/// <summary>
/// Implementación del servicio para gestión de roles
/// </summary>
public class RoleService : BaseService, IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger)
        : base(logger)
    {
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    }

    /// <summary>
    /// Crea un nuevo rol en el sistema
    /// </summary>
    public async Task<ApiResponse<RoleResponseDto>> CreateRoleAsync(CreateRoleRequestDto request, CancellationToken cancellationToken)
    {
        try
        {
            // Validar solicitud
            if (request == null)
                return ApiResponse<RoleResponseDto>.ErrorResponse("La solicitud es nula");

            if (string.IsNullOrWhiteSpace(request.Name))
                return ApiResponse<RoleResponseDto>.ErrorResponse("El nombre del rol es requerido");

            var name = request.Name.Trim();

            // Validar nombre único
            if (await _roleRepository.ExistsByNameAsync(name, cancellationToken))
            {
                _logger.LogWarning($"Intento de crear rol con nombre duplicado: {name}");
                return ApiResponse<RoleResponseDto>.ErrorResponse($"Ya existe un rol con el nombre '{name}'");
            }

            // Crear rol
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = request.Description?.Trim(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            // Guardar en BD
            var createdRole = await _roleRepository.CreateAsync(role, cancellationToken);

            // Mapear a DTO de respuesta
            var roleDto = MapRoleToDto(createdRole);

            _logger.LogInformation($"Rol creado exitosamente: {roleDto.Name} (ID: {roleDto.Id})");

            return ApiResponse<RoleResponseDto>.SuccessResponse(roleDto, "Rol creado exitosamente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear rol");
            return ApiResponse<RoleResponseDto>.ErrorResponse("Error al crear el rol");
        }
    }

    /// <summary>
    /// Obtiene todos los roles del sistema
    /// </summary>
    public async Task<ApiResponse<List<RoleResponseDto>>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _roleRepository.GetAllAsync(cancellationToken);
            var roleDtos = roles.Select(MapRoleToDto).ToList();

            return ApiResponse<List<RoleResponseDto>>.SuccessResponse(
                roleDtos,
                $"Se encontraron {roleDtos.Count} roles"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los roles");
            return ApiResponse<List<RoleResponseDto>>.ErrorResponse("Error al obtener los roles");
        }
    }

    /// <summary>
    /// Verifica si existe un rol con el nombre especificado
    /// </summary>
    public async Task<bool> RoleExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        return await _roleRepository.ExistsByNameAsync(name, cancellationToken);
    }

    /// <summary>
    /// Obtiene un rol por su ID
    /// </summary>
    public async Task<Role?> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            return null;

        return await _roleRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <summary>
    /// Obtiene un rol por su nombre
    /// </summary>
    public async Task<Role?> GetRoleByNameAsync(string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        return await _roleRepository.GetByNameAsync(name, cancellationToken);
    }

    /// <summary>
    /// Mapea una entidad Role a su DTO de respuesta
    /// </summary>
    private static RoleResponseDto MapRoleToDto(Role role)
    {
        return new RoleResponseDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            IsActive = role.IsActive,
            CreatedAt = role.CreatedAt
        };
    }
}
