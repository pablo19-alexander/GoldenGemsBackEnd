namespace GoldenGemsBackEnd.Models.People;

/// <summary>
/// Entidad Región (departamento y municipio).
/// </summary>
public class Region : BaseEntity
{
    /// <summary>
    /// Departamento.
    /// </summary>
    public string Department { get; set; } = string.Empty;

    /// <summary>
    /// Código del municipio.
    /// </summary>
    public string MunicipalityCode { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del municipio.
    /// </summary>
    public string MunicipalityName { get; set; } = string.Empty;

    /// <summary>
    /// Relación con contactos.
    /// </summary>
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
