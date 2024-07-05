using CliWrap;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Webion.Versioning.Tool.Contexts;
using Webion.Versioning.Tool.Core;
using Webion.Versioning.Tool.Data;
using Webion.Versioning.Tool.Managers.Files;
using Webion.Versioning.Tool.Ui;

namespace Webion.Versioning.Tool.Branches.Increment;

internal sealed class IncrementCommand : AsyncCommand<IncrementCommandSettings>
{
    private readonly VersioningDbContext _db;
    private readonly ICliApplicationLifetime _lifetime;
    private readonly IServiceProvider _serviceProvider;

    public IncrementCommand(VersioningDbContext db, ICliApplicationLifetime lifetime, IServiceProvider serviceProvider)
    {
        _db = db;
        _lifetime = lifetime;
        _serviceProvider = serviceProvider;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, IncrementCommandSettings settings)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync(_lifetime.CancellationToken);
        var app = await GetOrCreateAsync(settings.AppName);

        app.BuildCount = GetBuildCount(app, settings.Major, settings.Minor);
        app.BuildDate = DateTimeOffset.UtcNow;

        app.Major = settings.Major ?? app.Major;
        app.Minor = settings.Minor ?? app.Minor;
        
        await _db.SaveChangesAsync(_lifetime.CancellationToken);

        var updated = await UpdateFileAsync(settings, app);
        if (!updated)
            return 1;
        
        await transaction.CommitAsync(_lifetime.CancellationToken);

        await Cli.Wrap("git")
            .WithArguments(["tag", app.GetVersion()])
            .WithStandardOutputPipe(PipeTarget.ToDelegate(x => AnsiConsole.MarkupLine(Msg.Ok(x))))
            .WithStandardErrorPipe(PipeTarget.ToDelegate(x => AnsiConsole.MarkupLine(Msg.Err(x))))
            .WithValidation(CommandResultValidation.None)
            .ExecuteAsync(_lifetime.CancellationToken);
        
        AnsiConsole.MarkupLine(Msg.Ok($"[blue]{settings.AppName}[/] -> [b]{app.GetVersion()}[/]"));
        return 0;
    }
    
    
    private static uint GetBuildCount(AppDbo app, uint? major, uint? minor)
    {
        var lastBuildDate = DateOnly.FromDateTime(app.BuildDate.DateTime);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (lastBuildDate != today)
            return 1;

        if (major.HasValue && app.Major != major.Value)
            return 1;

        if (minor.HasValue && app.Minor != minor.Value)
            return 1;

        return app.BuildCount + 1;
    }

    private async Task<AppDbo> GetOrCreateAsync(string appName)
    {
        var app = await _db.Apps.FindAsync([appName], _lifetime.CancellationToken);
        if (app is not null)
            return app;

        var newApp = new AppDbo
        {
            Name = appName,
        };

        _db.Apps.Add(newApp);
        return newApp;
    }

    private async Task<bool> UpdateFileAsync(IncrementCommandSettings settings, AppDbo app)
    {
        if (settings.File is null || settings.Path is null)
        {
            AnsiConsole.MarkupLine(Msg.Warn("No file or path specified, skipping file version update"));
            return true;
        }
        
        var fileManager = _serviceProvider.GetRequiredKeyedService<IFileVersionManager>(settings.Lang);
        var done = await fileManager.WriteAsync(
            file: settings.File,
            path: settings.Path,
            version: app.ToString(),
            cancellationToken: _lifetime.CancellationToken
        );
        
        
        AnsiConsole.MarkupLine(done
            ? Msg.Ok("Version file updated correctly")
            : Msg.Err("Could not update version file")
        );

        return done;
    }
}