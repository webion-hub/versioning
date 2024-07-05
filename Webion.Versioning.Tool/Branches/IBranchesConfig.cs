using Spectre.Console.Cli;

namespace Webion.Versioning.Tool.Branches;

public interface IBranchConfig
{
    void Configure(IConfigurator config);
}