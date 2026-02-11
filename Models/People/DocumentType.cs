using GoldenGemsBackEnd.Models.People;

namespace GoldenGemsBackEnd.Models;

/// <summary>
/// Entidad para tipos de documento (Cédula, Pasaporte, etc.).
/// </summary>
public class DocumentType : BaseEntity
{
    /// <summary>
    /// Código del tipo de documento.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del tipo de documento.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Relación con las personas que tienen este tipo de documento.
    /// </summary>
    public ICollection<Person> People { get; set; } = new List<Person>();
}
