using Spectre.Console.Cli;
using Webion.Versioning.Tool.Branches.Get;
using Webion.Versioning.Tool.Branches.Increment;
using Webion.Versioning.Tool.Branches.List;
using Webion.Versioning.Tool.Branches.Tag;

namespace Webion.Versioning.Tool.Branches;

public sealed class MainBranch : IBranchConfig
{
    public void Configure(IConfigurator config)
    {
        config.AddCommand<IncrementCommand>("incr")
            .WithAlias("increment")
            .WithDescription("Increments an application's version");

        config.AddCommand<ListVersionsCommand>("list")
            .WithAlias("ls")
            .WithDescription("Lists all current applications with their respective version");
        
        config.AddCommand<TagCommand>("tag")
            .WithDescription("Tags the current git commit with the latest app version");

        config.AddCommand<GetCommand>("get")
            .WithDescription("Get the version associated with the given app name. Useful for scripts.");
    }
}