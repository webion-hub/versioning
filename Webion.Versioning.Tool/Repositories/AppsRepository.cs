namespace Webion.Versioning.Repositories;

internal sealed class AppsRepository
{
    public async Task<IList<AppDbo>> ListAllAsync(CancellationToken cancellationToken)
    {
        using var ctx = new VersioningDbContext();
        return await ctx.Apps
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<string> UpdateAsync(
        string appName,
        uint? major,
        uint? minor,
        CancellationToken cancellationToken
    )
    {
        using var ctx = new VersioningDbContext();
        var app = await GetOrCreateAsync(ctx, appName, cancellationToken);

        app.BuildCount = GetBuildCount(app, major, minor);
        app.BuildDate = DateOnly.FromDateTime(DateTime.UtcNow);

        if (major is not null)
            app.Major = major.Value;

        if (minor is not null)
            app.Minor = minor.Value;

        await ctx.SaveChangesAsync();
        return app.ToString();
    }


    private static uint GetBuildCount(AppDbo app, uint? major, uint? minor)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        if (app.BuildDate != today)
            return 1;

        if (major.HasValue && app.Major != major.Value)
            return 1;

        if (minor.HasValue && app.Minor != minor.Value)
            return 1;

        return app.BuildCount + 1;
    }

    private static async Task<AppDbo> GetOrCreateAsync(
        VersioningDbContext ctx,
        string appName,
        CancellationToken cancellationToken
    )
    {
        var app = await ctx.Apps.FindAsync(appName, cancellationToken);
        if (app is not null)
            return app;

        var newApp = new AppDbo
        {
            Name = appName,
        };

        ctx.Apps.Add(newApp);
        return newApp;
    }
}