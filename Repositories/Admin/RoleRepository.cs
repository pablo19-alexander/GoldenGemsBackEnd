using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Repositories.Admin;

/// <summary>
/// Implementación del repositorio para la entidad Role
/// </summary>
public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(GoldenGemsDbContext context) : base(context)
    {
    }

    public override async Task<Role> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        // Normalizar nombre (trim y lowercase para comparación)
        role.Name = role.Name.Trim();

        return await base.CreateAsync(role, cancellationToken);
    }

    public override async Task<Role> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        role.Name = role.Name.Trim();

        return await base.UpdateAsync(role, cancellationToken);
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
}
