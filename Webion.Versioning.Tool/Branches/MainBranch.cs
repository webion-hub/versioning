using Spectre.Console.Cli;
using Webion.Versioning.Tool.Branches.Increment;
using Webion.Versioning.Tool.Branches.List;

namespace Webion.Versioning.Tool.Branches;

public sealed class MainBranch : IBranchConfig
{
    public void Configure(IConfigurator config)
    {
        config.AddCommand<IncrementCommand>("incr")
            .WithAlias("increment");

        config.AddCommand<ListVersionsCommand>("list");
    }
}