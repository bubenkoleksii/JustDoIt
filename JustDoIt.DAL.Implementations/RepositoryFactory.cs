using JustDoIt.DAL.Implementations.Repositories;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;

namespace JustDoIt.DAL.Implementations;

public static class RepositoryFactory
{
    public static ICategoryRepository GetCategoryRepository(IServiceProvider serviceProvider, StorageType storageType)
    {
        var xmlConnectionFactory = (XmlConnectionFactory)serviceProvider.GetService(typeof(XmlConnectionFactory));
        var msSqlServerConnectionFactory =
            (MsSqlServerConnectionFactory)serviceProvider.GetService(typeof(MsSqlServerConnectionFactory));

        return storageType switch
        {
            StorageType.Xml => new CategoryXmlRepository(xmlConnectionFactory),
            StorageType.MsSqlServer => new CategoryMsSqlServerRepository(msSqlServerConnectionFactory),
            _ => throw new ArgumentException("Incorrect storage type")
        };
    }

    public static IJobRepository GetJobRepository(IServiceProvider serviceProvider, StorageType storageType)
    {
        var xmlConnectionFactory = (XmlConnectionFactory)serviceProvider.GetService(typeof(XmlConnectionFactory));
        var msSqlServerConnectionFactory =
            (MsSqlServerConnectionFactory)serviceProvider.GetService(typeof(MsSqlServerConnectionFactory));

        return storageType switch
        {
            StorageType.Xml => new JobXmlRepository(xmlConnectionFactory),
            StorageType.MsSqlServer => new JobMsSqlServerRepository(msSqlServerConnectionFactory),
            _ => throw new ArgumentException("Incorrect storage type")
        };
    }
}