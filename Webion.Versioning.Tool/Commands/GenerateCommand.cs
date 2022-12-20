namespace Webion.Versioning.Commands;

internal sealed class GenerateCommand : Command
{
    private readonly Option<uint?> majorOption = new("--major");
    private readonly Option<uint?> minorOption = new("--minor");
    private readonly Argument<string> nameArgument = new(
        name: "App name",
        description: "The app name"
    );


    public GenerateCommand() : base(
        name: "gen",
        description: "Generate a new app version"
    )
    {
        AddArgument(nameArgument);
        AddOption(majorOption);
        AddOption(minorOption);
        this.SetHandler(HandleAsync,
            nameArgument,
            majorOption,
            minorOption
        );
    }

    public async Task HandleAsync(string name, uint? major, uint? minor)
    {
        var apps = new AppsRepository();
        var version = await apps.UpdateAsync(
            appName: name,
            major: major,
            minor: minor,
            cancellationToken: default
        );

        Console.WriteLine(name);
        Console.WriteLine(version);
    }
}