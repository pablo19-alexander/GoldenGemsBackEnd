using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Repositories.Admin;

/// <summary>
/// Implementación del repositorio para la entidad Role
/// </summary>
public class RoleRepository : IRoleRepository
{
    private readonly GoldenGemsDbContext _context;

    public RoleRepository(GoldenGemsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Crea un nuevo rol en la base de datos
    /// </summary>
    public async Task<Role> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        // Normalizar nombre (trim y lowercase para comparación)
        role.Name = role.Name.Trim();

        _context.Roles.Add(role);
        await _context.SaveChangesAsync(cancellationToken);

        return role;
    }

    /// <summary>
    /// Obtiene todos los roles de la base de datos
    /// </summary>
    public async Task<List<Role>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene todos los roles activos
    /// </summary>
    public async Task<List<Role>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .Where(r => r.IsActive)
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Verifica si existe un rol con el nombre especificado
    /// </summary>
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        var normalizedName = name.Trim().ToLower();

        return await _context.Roles
            .AsNoTracking()
            .AnyAsync(r => r.Name.ToLower() == normalizedName, cancellationToken);
    }

    /// <summary>
    /// Obtiene un rol por su identificador único
    /// </summary>
    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    /// <summary>
    /// Obtiene un rol por su nombre
    /// </summary>
    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        var normalizedName = name.Trim().ToLower();

        return await _context.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name.ToLower() == normalizedName, cancellationToken);
    }

    /// <summary>
    /// Guarda los cambios en la base de datos
    /// </summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
