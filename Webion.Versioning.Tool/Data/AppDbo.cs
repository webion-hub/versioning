using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Webion.Versioning.Tool.Data;

public sealed class AppDbo : IEntityTypeConfiguration<AppDbo>
{
    public required string Name { get; set; }

    public uint Major { get; set; } = 1;
    public uint Minor { get; set; }
    public DateTimeOffset BuildDate { get; set; }
    public uint BuildCount { get; set; }

    public void Configure(EntityTypeBuilder<AppDbo> builder)
    {
        builder.ToTable("apps");
        builder.HasKey(x => x.Name);

        builder.Property(x => x.Major).IsRequired();
        builder.Property(x => x.Minor).IsRequired();
        builder.Property(x => x.BuildDate).IsRequired();
        builder.Property(x => x.BuildCount).IsRequired();
    }

    public override string ToString()
    {
        return GetVersion();
    }

    public string GetVersion()
    {
        return $"{Major}.{Minor}.{BuildCount}.{BuildDate:yy}{BuildDate.DayOfYear}.{BuildDate:HHmmss}";
    }
}