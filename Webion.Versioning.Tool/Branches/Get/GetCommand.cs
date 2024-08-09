using Spectre.Console;
using Spectre.Console.Cli;
using Webion.Versioning.Tool.Contexts;
using Webion.Versioning.Tool.Core;

namespace Webion.Versioning.Tool.Branches.Get;

public sealed class GetCommand : AsyncCommand<GetCommandSettings>
{
    private readonly VersioningDbContext _db;
    private readonly ICliApplicationLifetime _lifetime;

    public GetCommand(VersioningDbContext db, ICliApplicationLifetime lifetime)
    {
        _db = db;
        _lifetime = lifetime;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, GetCommandSettings settings)
    {
        var app = await _db.Apps
            .Where(x => x.Name == settings.AppName)
            .FirstOrDefaultAsync(_lifetime.CancellationToken);

        if (app is null)
            return -1;
        
        AnsiConsole.WriteLine(app.GetVersion());
        return 0;
    }
}