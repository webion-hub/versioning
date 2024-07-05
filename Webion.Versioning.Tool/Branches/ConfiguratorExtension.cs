using Spectre.Console.Cli;

namespace Webion.Versioning.Tool.Branches;

public static class ConfiguratorExtension
{
    public static void AddBranch<TConfig>(this IConfigurator config)
        where TConfig : IBranchConfig, new()
    {
        new TConfig().Configure(config);
    }
}