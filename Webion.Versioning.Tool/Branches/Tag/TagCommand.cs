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
        
        var result = await Cli.Wrap("git")
            .WithArguments(["tag", app.GetVersion()])
            .WithStandardOutputPipe(PipeTarget.ToDelegate(x => AnsiConsole.MarkupLine(Msg.Ok(x))))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(x => AnsiConsole.MarkupLine(Msg.Err(x))))
            .WithValidation(CommandResultValidation.None)
            .ExecuteAsync(_lifetime.CancellationToken);

        return result.ExitCode;
    }
}