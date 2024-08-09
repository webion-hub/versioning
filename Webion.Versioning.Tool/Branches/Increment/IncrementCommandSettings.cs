using System.ComponentModel;
using Spectre.Console.Cli;

namespace Webion.Versioning.Tool.Branches.Increment;

public sealed class IncrementCommandSettings : CommandSettings
{
    [CommandArgument(0, "<AppName>")]
    public string AppName { get; init; } = null!;
    
    [CommandOption("--major")]
    public uint? Major { get; init; }
    
    [CommandOption("--minor")]
    public uint? Minor { get; init; }
    
    [CommandOption("--file")]
    [Description("Path to the target file containing the version information.")]
    public string? File { get; init; }
    
    [CommandOption("--path")]
    [Description("Path to the version field to set inside the file.")]
    public string? Path { get; init; }
    
    [CommandOption("--lang")]
    public FileLanguage Lang { get; init; }
    
    [CommandOption("--format")]
    public string? Format { get; init; }
}