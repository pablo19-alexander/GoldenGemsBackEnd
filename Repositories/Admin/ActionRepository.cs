using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Repositories.Admin;

/// <summary>
/// Implementación del repositorio para la entidad Actions
/// </summary>
public class ActionRepository : GenericRepository<Actions>, IActionRepository
{
    public ActionRepository(GoldenGemsDbContext context) : base(context)
    {
    }

    public override async Task<Actions> CreateAsync(Actions action, CancellationToken cancellationToken)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        // Normalizar código (trim y uppercase para consistencia)
        action.Code = action.Code.Trim().ToUpper();
        action.Name = action.Name.Trim();

        return await base.CreateAsync(action, cancellationToken);
    }

    public override async Task<Actions> UpdateAsync(Actions action, CancellationToken cancellationToken)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        action.Code = action.Code.Trim().ToUpper();
        action.Name = action.Name.Trim();

        return await base.UpdateAsync(action, cancellationToken);
    }

    public override async Task<List<Actions>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(a => a.ActionType)
            .AsNoTracking()
            .OrderBy(a => a.Code)
            .ToListAsync(cancellationToken);
    }

    public override async Task<List<Actions>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(a => a.ActionType)
            .AsNoTracking()
            .Where(a => a.IsActive)
            .OrderBy(a => a.Code)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Actions?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(a => a.ActionType)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    /// <summary>
    /// Verifica si existe una acción con el código especificado
    /// </summary>
    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            return false;

        var normalizedCode = code.Trim().ToUpper();

        return await _context.Actions
            .AsNoTracking()
            .AnyAsync(a => a.Code.ToUpper() == normalizedCode, cancellationToken);
    }



    /// <summary>
    /// Obtiene una acción por su código
    /// </summary>
    public async Task<Actions?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            return null;

        var normalizedCode = code.Trim().ToUpper();

        return await _context.Actions
            .Include(a => a.ActionType)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Code.ToUpper() == normalizedCode, cancellationToken);
    }
}
