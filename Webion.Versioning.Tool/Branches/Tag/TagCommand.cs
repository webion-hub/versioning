using CliWrap;
using Spectre.Console;
using Spectre.Console.Cli;
using Webion.Versioning.Tool.Contexts;
using Webion.Versioning.Tool.Core;
using Webion.Versioning.Tool.Ui;

namespace Webion.Versioning.Tool.Branches.Tag;

public sealed class TagCommand : AsyncCommand<TagCommandSettings>
{
    private readonly VersioningDbContext _db;
    private readonly ICliApplicationLifetime _lifetime;

    public TagCommand(VersioningDbContext db, ICliApplicationLifetime lifetime)
    {
        _db = db;
        _lifetime = lifetime;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, TagCommandSettings settings)
    {
        var app = await _db.Apps.FindAsync([settings.AppName], _lifetime.CancellationToken);
        if (app is null)
        {
            AnsiConsole.MarkupLine(Msg.Err($"No app found with name {settings.AppName}"));
            return 1;
        }

        var git = Cli.Wrap("git")
            .WithStandardOutputPipe(PipeTarget.ToDelegate(AnsiConsole.WriteLine))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(AnsiConsole.WriteLine))
            .WithValidation(CommandResultValidation.None);
        
        var tagResult = await git
            .WithArguments(["tag", app.GetVersion()])
            .ExecuteAsync(_lifetime.CancellationToken);
        
        if (tagResult.IsSuccess)
            AnsiConsole.MarkupLine(Msg.Ok($"Commit tagged -> [b]{app.GetVersion()}[/]"));

        var pushResult = await git
            .WithArguments(["push", "origin", "tag", app.GetVersion()])
            .ExecuteAsync(_lifetime.CancellationToken);
        
        return pushResult.ExitCode;
    }
}