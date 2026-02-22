using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Repositories.Admin;

/// <summary>
/// Implementación del repositorio para la entidad ActionType.
/// </summary>
public class ActionTypeRepository : IActionTypeRepository
{
    private readonly GoldenGemsDbContext _context;

    public ActionTypeRepository(GoldenGemsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<ActionType>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ActionTypes
            .AsNoTracking()
            .OrderBy(at => at.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<ActionType?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ActionTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(at => at.Id == id, cancellationToken);
    }

    public async Task<ActionType?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            return null;

        var normalizedCode = code.Trim().ToUpper();

        return await _context.ActionTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(at => at.Code.ToUpper() == normalizedCode, cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            return false;

        return await _context.ActionTypes
            .AsNoTracking()
            .AnyAsync(at => at.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            return false;

        var normalizedCode = code.Trim().ToUpper();

        return await _context.ActionTypes
            .AsNoTracking()
            .AnyAsync(at => at.Code.ToUpper() == normalizedCode, cancellationToken);
    }
}
