using Spectre.Console.Cli;

namespace Webion.Versioning.Tool.Branches.Get;

public sealed class GetCommandSettings : CommandSettings
{
    [CommandArgument(0, "<app-name>")]
    public string AppName { get; init; } = null!;
    
    [CommandOption("--format")]
    public string? Format { get; init; }
}