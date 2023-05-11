using JustDoIt.DAL.Implementations.Repositories;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;

namespace JustDoIt.DAL.Implementations;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly MsSqlServerConnectionFactory _msSqlServerConnectionFactory;

    private readonly XmlConnectionFactory _xmlConnectionFactory;

    public RepositoryFactory(MsSqlServerConnectionFactory msSqlServerConnectionFactory,
        XmlConnectionFactory xmlConnectionFactory)
    {
        _msSqlServerConnectionFactory = msSqlServerConnectionFactory;
        _xmlConnectionFactory = xmlConnectionFactory;
    }

    public ICategoryRepository GetCategoryRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => new CategoryXmlRepository(_xmlConnectionFactory),
            StorageType.MsSqlServer => new CategoryMsSqlServerRepository(_msSqlServerConnectionFactory),
            _ => throw new ArgumentException("Incorrect storage type")
        };
    }

    public IJobRepository GetJobRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => new JobXmlRepository(_xmlConnectionFactory),
            StorageType.MsSqlServer => new JobMsSqlServerRepository(_msSqlServerConnectionFactory),
            _ => throw new ArgumentException("Incorrect storage type")
        };
    }
}