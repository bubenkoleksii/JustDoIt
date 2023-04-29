using Microsoft.Extensions.Configuration;

namespace JustDoIt.DAL.Implementations;

public class XmlFactory
{
    private readonly IConfiguration _configuration;
    private readonly string _storagePath;

    public XmlFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _storagePath = _configuration.GetConnectionString("XmlStoragePath");
    }

    public string GetStoragePath()
    {
        return _storagePath;
    }
}