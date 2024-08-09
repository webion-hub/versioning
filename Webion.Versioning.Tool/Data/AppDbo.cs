using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Webion.Versioning.Tool.Data;

public sealed class AppDbo : IEntityTypeConfiguration<AppDbo>
{
    public required string Name { get; set; }

    public uint Major { get; set; } = 1;
    public uint Minor { get; set; }
    public DateTimeOffset BuildDate { get; set; }
    public uint BuildCount { get; set; }
    public string UniqueId { get; set; } = null!;
    

    public void Configure(EntityTypeBuilder<AppDbo> builder)
    {
        builder.ToTable("apps");
        builder.HasKey(x => x.Name);

        builder.Property(x => x.Major).IsRequired();
        builder.Property(x => x.Minor).IsRequired();
        builder.Property(x => x.BuildDate).IsRequired();
        builder.Property(x => x.BuildCount).IsRequired();
        builder.Property(x => x.UniqueId).IsRequired();
    }
    
    public override string ToString()
    {
        return GetVersion();
    }

    
    private const string DefaultFormat = "{major}.{minor}.{buildDate:yy}{dayOfYear}.{buildCount}-{buildDate:HHmmss}-{uniqueId}";
    
    public string GetVersion(string? format = null)
    {
        var fmt = (format ?? DefaultFormat)
            .Replace("major", "0")
            .Replace("minor", "1")
            .Replace("buildDate", "2")
            .Replace("dayOfYear", "3")
            .Replace("buildCount", "4")
            .Replace("uniqueId", "5");
        
        return string.Format(fmt,
            Major,
            Minor,
            BuildDate,
            BuildDate.DayOfYear,
            BuildCount,
            UniqueId
        );
    }
}