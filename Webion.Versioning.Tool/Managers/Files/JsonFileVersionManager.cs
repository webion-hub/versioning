using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Webion.Versioning.Tool.Managers.Files;

public sealed class JsonFileVersionManager : IFileVersionManager
{
    private readonly ILogger<JsonFileVersionManager> _logger;

    public JsonFileVersionManager(ILogger<JsonFileVersionManager> logger)
    {
        _logger = logger;
    }

    public async Task<bool> WriteAsync(string file, string path, string version, CancellationToken cancellationToken)
    {
        if (!File.Exists(file))
        {
            _logger.LogInformation("File {File} does not exist, skipping", file);
            return false;
        }

        var text = await File.ReadAllTextAsync(file, cancellationToken);
        var root = JObject.Parse(text);
        var tokens = root.SelectTokens(path);

        foreach (var token in tokens)
        {
            if (token is not JValue value)
                continue;
            
            value.Value = version;
        }
        
        var json = JsonConvert.SerializeObject(root, Formatting.Indented);
        await File.WriteAllTextAsync(file, json, cancellationToken);
        return true;
    }

    public Task<string> ReadAsync(string file, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}