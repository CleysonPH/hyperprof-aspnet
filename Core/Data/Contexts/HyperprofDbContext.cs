using HyperProf.Core.Data.EntityConfigs;
using HyperProf.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HyperProf.Core.Data.Contexts;

public class HyperprofDbContext : DbContext
{
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<InvalidatedToken> InvalidatedTokens => Set<InvalidatedToken>();

    private readonly IConfiguration _configuration;

    public HyperprofDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is BaseModel
                &&
                (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

        foreach (var entityEntry in entries)
        {
            var model = (BaseModel)entityEntry.Entity;
            model.UpdatedAt = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                model.CreatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
        var connectionString = _configuration.GetConnectionString("MySqlConnection");
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TeacherEntityConfig());
        modelBuilder.ApplyConfiguration(new StudentEntityConfig());
        modelBuilder.ApplyConfiguration(new InvalidatedTokenEntityConfig());
    }
}