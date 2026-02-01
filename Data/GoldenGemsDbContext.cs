using Microsoft.EntityFrameworkCore;

namespace GoldenGemsBackEnd.Data;

public class GoldenGemsDbContext : DbContext
{
    public GoldenGemsDbContext(DbContextOptions<GoldenGemsDbContext> options)
        : base(options)
    {
    }

    // TODO: Agrega DbSet<TEntity> cuando tengas modelos.
    // public DbSet<Entidad> Entidades { get; set; } = default!;
}
