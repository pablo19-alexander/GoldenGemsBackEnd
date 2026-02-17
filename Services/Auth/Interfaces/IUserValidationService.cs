using GoldenGemsBackEnd.DTOs.Auth;

namespace GoldenGemsBackEnd.Services.Auth.Interfaces;

/// <summary>
/// Interfaz del servicio de validación de usuarios
/// </summary>
public interface IUserValidationService
{
    /// <summary>
    /// Valida que un email sea único en el sistema
    /// </summary>
    /// <param name="email">Email a validar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el email es único, false si ya existe</returns>
    Task<bool> ValidateEmailUniqueAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Valida que un username sea único en el sistema
    /// </summary>
    /// <param name="username">Username a validar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el username es único, false si ya existe</returns>
    Task<bool> ValidateUsernameUniqueAsync(string username, CancellationToken cancellationToken);

    /// <summary>
    /// Valida la complejidad de una contraseña
    /// </summary>
    /// <param name="password">Contraseña a validar</param>
    /// <returns>true si cumple requisitos, false en caso contrario</returns>
    bool ValidatePasswordStrength(string password);

    /// <summary>
    /// Obtiene los errores de validación de una contraseña
    /// </summary>
    /// <param name="password">Contraseña a validar</param>
    /// <returns>Lista de errores de validación</returns>
    List<string> GetPasswordValidationErrors(string password);

    /// <summary>
    /// Valida que un número de documento sea único para su tipo
    /// </summary>
    /// <param name="documentNumber">Número de documento a validar</param>
    /// <param name="documentTypeId">ID del tipo de documento</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el documento es único, false si ya existe</returns>
    Task<bool> ValidateDocumentNumberUniqueAsync(string documentNumber, Guid documentTypeId, CancellationToken cancellationToken);

    /// <summary>
    /// Valida que un rol exista en el sistema
    /// </summary>
    /// <param name="roleId">ID del rol a validar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el rol existe, false en caso contrario</returns>
    Task<bool> ValidateRoleExistsAsync(Guid roleId, CancellationToken cancellationToken);

    /// <summary>
    /// Valida que un tipo de documento exista en el sistema
    /// </summary>
    /// <param name="documentTypeId">ID del tipo de documento a validar</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>true si el tipo existe, false en caso contrario</returns>
    Task<bool> ValidateDocumentTypeExistsAsync(Guid documentTypeId, CancellationToken cancellationToken);
}
