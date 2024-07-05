using System.Xml;
using Microsoft.Extensions.Logging;

namespace Webion.Versioning.Tool.Managers.Files;

public sealed class XmlFileVersionManager : IFileVersionManager
{
    private readonly ILogger<XmlFileVersionManager> _logger;

    public XmlFileVersionManager(ILogger<XmlFileVersionManager> logger)
    {
        _logger = logger;
    }

    public Task<bool> WriteAsync(string file, string path, string version, CancellationToken cancellationToken)
    {
        if (!File.Exists(file))
        {
            _logger.LogInformation("File {File} does not exist, skipping", file);
            return Task.FromResult(false);
        }
        
        var doc = new XmlDocument();
        doc.Load(file);
        var root = doc.DocumentElement;

        if (root is null)
        {
            _logger.LogInformation("Could not find xml document root for file {File}", file);
            return Task.FromResult(false);
        }
        
        var nsManager = new XmlNamespaceManager(doc.NameTable);
        
        var nodes = root.SelectNodes(path, nsManager);
        if (nodes is null)
        {
            _logger.LogInformation("No nodes found matching {Path}", path);
            return Task.FromResult(true);
        }
        
        foreach (XmlNode node in nodes)
        {
            _logger.LogInformation("{Node}: {Xml}", node.Name, node.InnerXml);
            node.InnerXml = version;
        }

        doc.Save(file);
        return Task.FromResult(true);
    }

    public Task<string> ReadAsync(string file, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}