using Microsoft.Extensions.Configuration;

namespace JustDoIt.DAL.Implementations;

public class XmlConnectionFactory
{
    private readonly string _categoryStoragePath;

    private readonly string _jobStoragePath;
    private readonly string _storageFolderPath;

    public XmlConnectionFactory(IConfiguration configuration)
    {
        _storageFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName,
            configuration.GetConnectionString("XmlStoragePath"));

        _jobStoragePath = configuration.GetConnectionString("XmlJobStoragePath");
        _categoryStoragePath = configuration.GetConnectionString("XmlCategoryStoragePath");
    }

    public string GetJobStoragePath()
    {
        var jobStoragePath = Path.Combine(_storageFolderPath, _jobStoragePath);
        return jobStoragePath;
    }

    public string GetCategoryStoragePath()
    {
        var categoryStoragePath = Path.Combine(_storageFolderPath, _categoryStoragePath);
        return categoryStoragePath;
    }
}