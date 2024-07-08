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
        _logger.LogInformation("Loading json file {File}", file);
        if (!File.Exists(file))
        {
            _logger.LogInformation("File does not exist, skipping");
            return false;
        }

        var text = await File.ReadAllTextAsync(file, cancellationToken);
        var root = JObject.Parse(text);
        _logger.LogInformation("Loaded json file correctly");
        _logger.LogTrace("File contents {Contents}", text);
        
        var tokens = root
            .SelectTokens(path)
            .ToList();
        
        _logger.LogInformation("Matched tokens {@Tokens} for path {Path}",
            tokens.Select(x => x.Path),
            path
        );

        foreach (var token in tokens)
        {
            _logger.LogDebug("Handling token {Token}", token.Path);
            if (token is not JValue value)
            {
                _logger.LogDebug("Not a json value, skipping");
                continue;
            }
            
            value.Value = version;
        }
        
        var json = JsonConvert.SerializeObject(root, Formatting.Indented);
        await File.WriteAllTextAsync(file, json, cancellationToken);
        _logger.LogInformation("Saved updated json to file");
        
        return true;
    }

    public Task<string> ReadAsync(string file, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}