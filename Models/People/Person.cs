namespace GoldenGemsBackEnd.Models.People;

/// <summary>
/// Entidad Persona con datos personales del usuario.
/// </summary>
public class Person : BaseEntity
{
    /// <summary>
    /// Primer nombre.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Segundo nombre.
    /// </summary>
    public string SecondName { get; set; } = string.Empty;

    /// <summary>
    /// Primer apellido.
    /// </summary>
    public string FirstLastName { get; set; } = string.Empty;

    /// <summary>
    /// Segundo apellido.
    /// </summary>
    public string SecondLastName { get; set; } = string.Empty;

    /// <summary>
    /// Número de documento.
    /// </summary>
    public string DocumentNumber { get; set; } = string.Empty;

    /// <summary>
    /// Relación con el tipo de documento.
    /// </summary>
    public Guid DocumentTypeId { get; set; }

    /// <summary>
    /// Relación con contacto.
    /// </summary>
    public Guid? ContactId { get; set; }
    
    /// <summary>
    /// Relación con el usuario.
    /// </summary>
    public Guid UserId { get; set; }


    public DocumentType? DocumentType { get; set; }
    public Contact? Contact { get; set; }
    public User? User { get; set; }
}
