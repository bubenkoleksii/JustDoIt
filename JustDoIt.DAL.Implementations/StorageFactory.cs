using JustDoIt.DAL.Implementations.Repositories;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;
using Microsoft.Extensions.Configuration;

namespace JustDoIt.DAL.Implementations;

public class StorageFactory : IStorageFactory
{
    private readonly MsSqlServerConnectionFactory _msSqlServerConnectionFactory;

    private readonly XmlConnectionFactory _xmlConnectionFactory;

    public StorageFactory(MsSqlServerConnectionFactory msSqlServerConnectionFactory, XmlConnectionFactory xmlConnectionFactory)
    {
        _msSqlServerConnectionFactory = msSqlServerConnectionFactory;
        _xmlConnectionFactory = xmlConnectionFactory;
    }

    public ICategoryRepository GetCategoryRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => new CategoryXmlRepository(_xmlConnectionFactory),
            StorageType.MsSqlServer => new CategoryMsSqlServerRepository(_msSqlServerConnectionFactory)
        };
    }

    public IJobRepository GetJobRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => new JobXmlRepository(_xmlConnectionFactory),
            StorageType.MsSqlServer => new JobMsSqlServerRepository(_msSqlServerConnectionFactory)
        };
    }
}