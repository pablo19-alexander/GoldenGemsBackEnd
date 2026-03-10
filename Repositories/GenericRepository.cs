using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GoldenGemsBackEnd.Repositories;

/// <summary>
/// Implementación genérica del patrón Repository para proporcionar operaciones CRUD básicas.
/// </summary>
/// <typeparam name="T">Tipo de entidad que hereda de BaseEntity</typeparam>
public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly GoldenGemsDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(GoldenGemsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    /// <inheritdoc />
    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <inheritdoc />
    public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<List<T>> GetAllActiveAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(e => e.IsActive)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(e => e.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public virtual async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        // Implementación de Soft Delete
        entity.IsActive = false;
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
