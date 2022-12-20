using Webion.Versioning.Commands;

using (var ctx = new VersioningDbContext())
{
    await ctx.Database.EnsureCreatedAsync();
}


var rootCommand = new RootCommand();
var listCommand = new ListCommand();
var updateCommand = new UpdateCommand();


rootCommand.AddCommand(listCommand);
rootCommand.AddCommand(updateCommand);

return await rootCommand.InvokeAsync(args);