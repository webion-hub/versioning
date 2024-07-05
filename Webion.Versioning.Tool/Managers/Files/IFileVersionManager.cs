namespace Webion.Versioning.Tool.Managers.Files;

public interface IFileVersionManager
{
    Task<bool> WriteAsync(string file, string path, string version, CancellationToken cancellationToken);
    Task<string> ReadAsync(string file, string path, CancellationToken cancellationToken);
}