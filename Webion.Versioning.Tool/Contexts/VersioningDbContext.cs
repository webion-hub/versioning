namespace Webion.Versioning.Contexts;

internal sealed class VersioningDbContext : DbContext
{
    public DbSet<AppDbo> Apps { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        Directory.CreateDirectory("~/wv");
        builder.UseSqlite("Filename=~/wv/db.sqlite");
    }
}