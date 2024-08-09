using Spectre.Console;
using Spectre.Console.Cli;
using Webion.Versioning.Tool.Contexts;
using Webion.Versioning.Tool.Core;

namespace Webion.Versioning.Tool.Branches.List;

public sealed class ListVersionsCommand : AsyncCommand<ListVersionsCommandSettings>
{
    private readonly VersioningDbContext _db;
    private readonly ICliApplicationLifetime _lifetime;

    public ListVersionsCommand(ICliApplicationLifetime lifetime, VersioningDbContext db)
    {
        _lifetime = lifetime;
        _db = db;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ListVersionsCommandSettings settings)
    {
        var apps = await _db.Apps.ToListAsync(_lifetime.CancellationToken);
        var table = new Table();
        table.AddColumns("App", "Version");
        
        foreach (var app in apps)
            table.AddRow(app.Name, app.GetVersion());
        
        AnsiConsole.Write(table);
        return 0;
    }
}