using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Admin.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Repositories.Admin;

/// <summary>
/// Implementación del repositorio para la entidad Actions
/// </summary>
public class ActionRepository : IActionRepository
{
    private readonly GoldenGemsDbContext _context;

    public ActionRepository(GoldenGemsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Crea una nueva acción en la base de datos
    /// </summary>
    public async Task<Actions> CreateAsync(Actions action, CancellationToken cancellationToken)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        // Normalizar código (trim y uppercase para consistencia)
        action.Code = action.Code.Trim().ToUpper();
        action.Name = action.Name.Trim();

        _context.Actions.Add(action);
        await _context.SaveChangesAsync(cancellationToken);

        return action;
    }

    /// <summary>
    /// Obtiene todas las acciones de la base de datos
    /// </summary>
    public async Task<List<Actions>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Actions
            .Include(a => a.ActionType)
            .AsNoTracking()
            .OrderBy(a => a.Code)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Obtiene todas las acciones activas
    /// </summary>
    public async Task<List<Actions>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await _context.Actions
            .Include(a => a.ActionType)
            .AsNoTracking()
            .Where(a => a.IsActive)
            .OrderBy(a => a.Code)
            .ToListAsync(cancellationToken);
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
    /// Obtiene una acción por su identificador único
    /// </summary>
    public async Task<Actions?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Actions
            .Include(a => a.ActionType)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
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

    /// <summary>
    /// Guarda los cambios en la base de datos
    /// </summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
