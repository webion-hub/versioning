namespace Webion.Versioning.Tool.Core;

public interface ICliApplicationLifetime
{
    CancellationToken CancellationToken { get; }
}