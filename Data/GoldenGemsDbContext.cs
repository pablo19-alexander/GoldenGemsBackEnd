using GoldenGemsBackEnd.Models.Security;
using GoldenGemsBackEnd.Models.People;
using Microsoft.EntityFrameworkCore;
using GoldenGemsBackEnd.Models;

namespace GoldenGemsBackEnd.Data;

public class GoldenGemsDbContext : DbContext
{
    public GoldenGemsDbContext(DbContextOptions<GoldenGemsDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Tabla de Usuarios del sistema.
    /// </summary>
    public DbSet<User> Users { get; set; } = default!;

    /// <summary>
    /// Tabla de Roles disponibles en el sistema.
    /// </summary>
    public DbSet<Role> Roles { get; set; } = default!;

    /// <summary>
    /// Tabla de Módulos del sistema.
    /// </summary>
    public DbSet<Module> Modules { get; set; } = default!;

    /// <summary>
    /// Tabla de Formularios del sistema.
    /// </summary>
    public DbSet<Form> Forms { get; set; } = default!;

    /// <summary>
    /// Tabla de Acciones disponibles (módulos, formularios, procesos).
    /// </summary>
    public DbSet<Actions> Actions { get; set; } = default!;

    /// <summary>
    /// Catálogo de tipos de acción disponibles.
    /// </summary>
    public DbSet<ActionType> ActionTypes { get; set; } = default!;

    /// <summary>
    /// Tabla de relación entre Roles y Acciones.
    /// </summary>
    public DbSet<RoleAction> RoleActions { get; set; } = default!;

    /// <summary>
    /// Tabla de relación entre Usuarios y Roles.
    /// </summary>
    public DbSet<UserRole> UserRoles { get; set; } = default!;

    /// <summary>
    /// Tabla de Personas.
    /// </summary>
    public DbSet<Person> People { get; set; } = default!;

    /// <summary>
    /// Tabla de Contactos.
    /// </summary>
    public DbSet<Contact> Contacts { get; set; } = default!;

    /// <summary>
    /// Tabla de Regiones.
    /// </summary>
    public DbSet<Region> Regions { get; set; } = default!;

    /// <summary>
    /// Tabla de Tipos de Documento.
    /// </summary>  
    public DbSet<DocumentType> DocumentTypes { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar índices únicos
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Role>()
            .HasIndex(r => r.Name)
            .IsUnique();

        modelBuilder.Entity<Module>()
            .HasIndex(m => m.Code)
            .IsUnique();

        modelBuilder.Entity<Form>()
            .HasIndex(f => f.Code)
            .IsUnique();

        modelBuilder.Entity<Actions>()
            .HasIndex(a => a.Code)
            .IsUnique();

        modelBuilder.Entity<ActionType>()
            .HasIndex(at => at.Code)
            .IsUnique();

        // Configurar relaciones - Forms
        modelBuilder.Entity<Form>()
            .HasOne(f => f.Module)
            .WithMany(m => m.Forms)
            .HasForeignKey(f => f.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar relaciones - Actions
        modelBuilder.Entity<Actions>()
            .HasOne(a => a.Module)
            .WithMany(m => m.Actions)
            .HasForeignKey(a => a.ModuleId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Actions>()
            .HasOne(a => a.Form)
            .WithMany(f => f.Actions)
            .HasForeignKey(a => a.FormId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Actions>()
            .HasOne(a => a.ActionType)
            .WithMany(at => at.Actions)
            .HasForeignKey(a => a.ActionTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar relaciones - UserRole
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar relaciones - RoleAction
        modelBuilder.Entity<RoleAction>()
            .HasOne(ra => ra.Role)
            .WithMany(r => r.RoleActions)
            .HasForeignKey(ra => ra.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RoleAction>()
            .HasOne(ra => ra.Action)
            .WithMany(a => a.RoleActions)
            .HasForeignKey(ra => ra.ActionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar relaciones - Person
        modelBuilder.Entity<Person>()
            .HasOne(p => p.User)
            .WithOne(u => u.Person)
            .HasForeignKey<Person>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Person>()
            .HasOne(p => p.DocumentType)
            .WithMany(dt => dt.People)
            .HasForeignKey(p => p.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Person>()
            .HasOne(p => p.Contact)
            .WithMany(c => c.People)
            .HasForeignKey(p => p.ContactId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configurar relaciones - Contact
        modelBuilder.Entity<Contact>()
            .HasOne(c => c.Region)
            .WithMany(r => r.Contacts)
            .HasForeignKey(c => c.RegionId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
