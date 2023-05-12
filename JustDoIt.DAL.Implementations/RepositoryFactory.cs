using JustDoIt.DAL.Implementations.Repositories;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;

namespace JustDoIt.DAL.Implementations;

public static class RepositoryFactory
{
    public static ICategoryRepository GetCategoryRepository(XmlConnectionFactory xmlConnectionFactory, MsSqlServerConnectionFactory msSqlServerConnectionFactory, StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => new CategoryXmlRepository(xmlConnectionFactory),
            StorageType.MsSqlServer => new CategoryMsSqlServerRepository(msSqlServerConnectionFactory),
            _ => throw new ArgumentException("Incorrect storage type")
        };
    }

    public static IJobRepository GetJobRepository(XmlConnectionFactory xmlConnectionFactory, MsSqlServerConnectionFactory msSqlServerConnectionFactory, StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => new JobXmlRepository(xmlConnectionFactory),
            StorageType.MsSqlServer => new JobMsSqlServerRepository(msSqlServerConnectionFactory),
            _ => throw new ArgumentException("Incorrect storage type")
        };
    }
}