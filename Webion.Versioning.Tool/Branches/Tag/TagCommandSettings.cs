using Spectre.Console.Cli;

namespace Webion.Versioning.Tool.Branches.Tag;

public sealed class TagCommandSettings : CommandSettings
{
    [CommandArgument(0, "<AppName>")]
    public string AppName { get; init; } = null!;
}