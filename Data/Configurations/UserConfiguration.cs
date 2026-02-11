using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GoldenGemsBackEnd.Models.Security;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasComment("Tabla de Usuarios del sistema...");
        
        builder.Property(u => u.Id)
            .HasComment("Identificador único del usuario...")
            .ValueGeneratedOnAdd();
            
        // ...resto de configuración...
    }
}
