namespace Webion.Versioning.Data;

internal sealed class AppDbo
{
    [Key]
    [Required]
    public required string Name { get; set; }

    [Required] public uint Major { get; set; } = 1;
    [Required] public uint Minor { get; set; } = 0;
    [Required] public DateOnly BuildDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    [Required] public uint BuildCount { get; set; } = 0;


    public override string ToString()
    {
        return $"{Major}.{Minor}.{BuildDate:yy}{BuildDate.DayOfYear}.{BuildCount}";
    }
}