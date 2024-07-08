using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;
using SpectreConsoleLogger;
using Webion.Versioning.Tool.Branches;
using Webion.Versioning.Tool.Branches.Increment;
using Webion.Versioning.Tool.Contexts;
using Webion.Versioning.Tool.Core;
using Webion.Versioning.Tool.DI;
using Webion.Versioning.Tool.Managers.Files;

await using (var db = new VersioningDbContext())
{
    await db.Database.MigrateAsync();
}

var config = new ConfigurationBuilder()
    .AddCommandLine(args)
    .Build();

var services = new ServiceCollection();

services.AddLogging(options =>
{
    options.ClearProviders();
    options.AddProvider(new SpectreConsoleLoggerProvider());
    
    var logLevel = config.GetValue("log-level", LogLevel.Error);
    options.SetMinimumLevel(logLevel);
});

services.AddSingleton<ICliApplicationLifetime, CliApplicationLifetime>();
services.AddKeyedTransient<IFileVersionManager, JsonFileVersionManager>(FileLanguage.Json);
services.AddKeyedTransient<IFileVersionManager, XmlFileVersionManager>(FileLanguage.Xml);

services.AddDbContext<VersioningDbContext>();

var registrar = new TypeRegistrar(services);
var app = new CommandApp(registrar);

app.Configure(o =>
{
    var fullErrors = config.GetValue("full-errors", false);
    if (fullErrors)
        o.PropagateExceptions();

    o.UseAssemblyInformationalVersion();
    o.AddBranch<MainBranch>();
});

return await app.RunAsync(args);