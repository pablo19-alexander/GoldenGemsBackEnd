using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Repositories.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Repositories.Auth;

/// <summary>
/// Implementación del repositorio para la entidad User
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly GoldenGemsDbContext _context;

    public UserRepository(GoldenGemsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Crea un nuevo usuario en la base de datos
    /// </summary>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        // Normalizar email y username (trim y lowercase)
        user.Email = user.Email.Trim().ToLower();
        user.Username = user.Username.Trim();

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }

    /// <summary>
    /// Obtiene un usuario por su email
    /// </summary>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        var normalizedEmail = email.Trim().ToLower();

        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
    }

    /// <summary>
    /// Obtiene un usuario por su username
    /// </summary>
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(username))
            return null;

        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username.Trim(), cancellationToken);
    }

    /// <summary>
    /// Obtiene un usuario por su ID incluyendo sus roles relacionados
    /// </summary>
    public async Task<User?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            return null;

        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    /// <summary>
    /// Verifica si existe un usuario con el email especificado
    /// </summary>
    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        var normalizedEmail = email.Trim().ToLower();

        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email.ToLower() == normalizedEmail, cancellationToken);
    }

    /// <summary>
    /// Verifica si existe un usuario con el username especificado
    /// </summary>
    public async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(username))
            return false;

        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Username == username.Trim(), cancellationToken);
    }

    /// <summary>
    /// Guarda los cambios en la base de datos
    /// </summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
