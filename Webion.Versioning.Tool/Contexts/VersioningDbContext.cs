using Webion.Versioning.Tool.Data;

namespace Webion.Versioning.Tool.Contexts;

public sealed class VersioningDbContext : DbContext
{
    public DbSet<AppDbo> Apps { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VersioningDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var baseDir = $"{home}/.wv";
        
        Directory.CreateDirectory(baseDir);
        builder.UseSqlite($"Filename={baseDir}/db.sqlite");
    }
}