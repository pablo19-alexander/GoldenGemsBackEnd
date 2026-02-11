namespace GoldenGemsBackEnd.Models.People;

/// <summary>
/// Entidad Contacto.
/// </summary>
public class Contact : BaseEntity
{
    /// <summary>
    /// Celular.
    /// </summary>
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Dirección.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Barrio.
    /// </summary>
    public string Neighborhood { get; set; } = string.Empty;

    /// <summary>
    /// Relación con región.
    /// </summary>
    public Guid? RegionId { get; set; }
    public Region? Region { get; set; }

    /// <summary>
    /// Relación inversa con personas.
    /// </summary>
    public ICollection<Person> People { get; set; } = new List<Person>();
}
