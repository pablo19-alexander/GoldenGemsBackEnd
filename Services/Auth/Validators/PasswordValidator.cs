using System.Text.RegularExpressions;

namespace GoldenGemsBackEnd.Services.Auth.Validators;

/// <summary>
/// Validador de contraseñas fuertes
/// Implementa validaciones de complejidad de contraseña
/// </summary>
public static class PasswordValidator
{
    private const int MinimumLength = 8;
    private const string SpecialCharacters = "!@#$%^&*";

    /// <summary>
    /// Valida que una contraseña cumpla con los requisitos de complejidad:
    /// - Mínimo 8 caracteres
    /// - Al menos 1 mayúscula [A-Z]
    /// - Al menos 1 minúscula [a-z]
    /// - Al menos 1 número [0-9]
    /// - Al menos 1 carácter especial [!@#$%^&*]
    /// </summary>
    /// <param name="password">Contraseña a validar</param>
    /// <returns>Tupla con (isValid: bool, errors: List<string>)</returns>
    public static (bool isValid, List<string> errors) Validate(string password)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(password))
        {
            errors.Add("La contraseña no puede estar vacía");
            return (false, errors);
        }

        if (password.Length < MinimumLength)
        {
            errors.Add($"La contraseña debe tener al menos {MinimumLength} caracteres");
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            errors.Add("La contraseña debe contener al menos una mayúscula (A-Z)");
        }

        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            errors.Add("La contraseña debe contener al menos una minúscula (a-z)");
        }

        if (!Regex.IsMatch(password, @"[0-9]"))
        {
            errors.Add("La contraseña debe contener al menos un número (0-9)");
        }

        if (!Regex.IsMatch($@"[{Regex.Escape(SpecialCharacters)}]", password))
        {
            errors.Add($"La contraseña debe contener al menos un carácter especial ({SpecialCharacters})");
        }

        return (errors.Count == 0, errors);
    }

    /// <summary>
    /// Verifica si una contraseña es válida
    /// </summary>
    /// <param name="password">Contraseña a verificar</param>
    /// <returns>true si es válida, false en caso contrario</returns>
    public static bool IsValid(string password)
    {
        var (isValid, _) = Validate(password);
        return isValid;
    }

    /// <summary>
    /// Obtiene los errores de validación de una contraseña
    /// </summary>
    /// <param name="password">Contraseña a validar</param>
    /// <returns>Lista de errores de validación</returns>
    public static List<string> GetErrors(string password)
    {
        var (_, errors) = Validate(password);
        return errors;
    }
}
