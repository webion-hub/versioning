using Webion.Versioning.Commands;

using (var ctx = new VersioningDbContext())
{
    await ctx.Database.EnsureCreatedAsync();
}


var rootCommand = new RootCommand();
var listCommand = new ListCommand();
var generateCommand = new GenerateCommand();


rootCommand.AddCommand(listCommand);
rootCommand.AddCommand(generateCommand);

return await rootCommand.InvokeAsync(args);