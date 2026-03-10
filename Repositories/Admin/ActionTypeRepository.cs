using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Repositories.Admin;

/// <summary>
/// Implementación del repositorio para la entidad ActionType.
/// </summary>
public class ActionTypeRepository : GenericRepository<ActionType>, IActionTypeRepository
{
    public ActionTypeRepository(GoldenGemsDbContext context) : base(context)
    {
    }



    public async Task<ActionType?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            return null;

        var normalizedCode = code.Trim().ToUpper();

        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(at => at.Code.ToUpper() == normalizedCode, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            return false;

        var normalizedCode = code.Trim().ToUpper();

        return await _dbSet
            .AsNoTracking()
            .AnyAsync(at => at.Code.ToUpper() == normalizedCode, cancellationToken);
    }


}
