using GoldenGemsBackEnd.Data;
using GoldenGemsBackEnd.DTOs;
using GoldenGemsBackEnd.DTOs.Auth;
using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Services.Auth.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly GoldenGemsDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(
            GoldenGemsDbContext context,
            ITokenService tokenService,
            IPasswordHasher<User> passwordHasher,
            ILogger<AuthService> logger) : base(logger)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Registra un nuevo usuario validando duplicados, generando hash y emitiendo un JWT listo para consumir.
        /// </summary>
        /// <param name="request">Datos de registro enviados por el cliente.</param>
        /// <param name="cancellationToken">Token para cancelar la operación asincrónica.</param>
        /// <returns>Respuesta estandarizada con datos del usuario y token o errores de validación.</returns>
        public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = request.Email.Trim();
            var normalizedUsername = request.Username.Trim();

            if (await _context.Users.AnyAsync(u => u.Email == normalizedEmail, cancellationToken))
            {
                return ApiResponse<AuthResponseDto>.ErrorResponse("El correo electrónico ya está registrado.");
            }

            if (await _context.Users.AnyAsync(u => u.Username == normalizedUsername, cancellationToken))
            {
                return ApiResponse<AuthResponseDto>.ErrorResponse("El nombre de usuario ya está en uso.");
            }

            var user = new User
            {
                Email = normalizedEmail,
                Username = normalizedUsername
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.RoleIds?.Any() == true)
            {
                var rolesToAssign = await _context.Roles
                    .Where(r => request.RoleIds!.Contains(r.Id))
                    .Select(r => r.Id)
                    .ToListAsync(cancellationToken);

                foreach (var roleId in rolesToAssign)
                {
                    await _context.UserRoles.AddAsync(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = roleId
                    }, cancellationToken);
                }

                if (rolesToAssign.Any())
                {
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            var userWithRoles = await LoadUserWithRolesAsync(user.Id, cancellationToken);
            return BuildAuthSuccessResponse(userWithRoles, "Usuario registrado correctamente.");
        }

        /// <summary>
        /// Autentica un usuario por email o username, verifica la contraseña y devuelve un JWT si es válida.
        /// </summary>
        /// <param name="request">Credenciales proporcionadas por el cliente.</param>
        /// <param name="cancellationToken">Token opcional para cancelar la operación.</param>
        /// <returns>Respuesta con el token emitido o errores de autenticación.</returns>
        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
        {
            var identifier = request.Identifier.Trim();

            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == identifier || u.Username == identifier, cancellationToken);

            if (user == null)
            {
                return ApiResponse<AuthResponseDto>.ErrorResponse("Credenciales inválidas.");
            }

            var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verification == PasswordVerificationResult.Failed)
            {
                return ApiResponse<AuthResponseDto>.ErrorResponse("Credenciales inválidas.");
            }

            if (verification == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return BuildAuthSuccessResponse(user, "Autenticación exitosa.");
        }

        /// <summary>
        /// Recupera el usuario junto con la colección de roles asociados para construir la respuesta completa.
        /// </summary>
        /// <param name="userId">Identificador del usuario recién creado o autenticado.</param>
        /// <param name="cancellationToken">Token opcional para cancelar la consulta.</param>
        private async Task<User> LoadUserWithRolesAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstAsync(u => u.Id == userId, cancellationToken);
        }

        /// <summary>
        /// Construye la respuesta de autenticación exitosa generando el JWT y formateando el payload.
        /// </summary>
        /// <param name="user">Entidad usuario con roles cargados.</param>
        /// <param name="message">Mensaje amigable para el cliente.</param>
        private ApiResponse<AuthResponseDto> BuildAuthSuccessResponse(User user, string message)
        {
            var roleNames = user.UserRoles
                .Select(ur => ur.Role?.Name ?? string.Empty)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var tokenResult = _tokenService.GenerateToken(user, roleNames);

            var payload = new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.Username,
                Roles = roleNames,
                Token = tokenResult.Token,
                ExpiresAt = tokenResult.ExpiresAt
            };

            return ApiResponse<AuthResponseDto>.SuccessResponse(payload, message);
        }
    }
}
