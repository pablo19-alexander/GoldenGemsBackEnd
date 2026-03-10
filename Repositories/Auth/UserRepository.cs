using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Repositories.Auth;

/// <summary>
/// Implementación del repositorio para la entidad User
/// </summary>
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(GoldenGemsDbContext context) : base(context)
    {
    }

    public override async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        // Normalizar email y username (trim y lowercase)
        user.Email = user.Email.Trim().ToLower();
        user.Username = user.Username.Trim();

        return await base.CreateAsync(user, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        var normalizedEmail = email.Trim().ToLower();

        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(username))
            return null;

        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username.Trim(), cancellationToken);
    }

    public async Task<User?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            return null;

        return await _dbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        var normalizedEmail = email.Trim().ToLower();

        return await _dbSet
            .AsNoTracking()
            .AnyAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
    }

    public async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(username))
            return false;

        return await _dbSet
            .AsNoTracking()
            .AnyAsync(u => u.Username == username.Trim(), cancellationToken);
    }
}
