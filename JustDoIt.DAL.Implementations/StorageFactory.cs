using JustDoIt.DAL.Implementations.Repositories;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;
using Microsoft.Extensions.Configuration;

namespace JustDoIt.DAL.Implementations;

public class StorageFactory : IStorageFactory
{
    private readonly IConfiguration _configuration;

    private readonly MsSqlServerFactory _msSqlServerFactory;

    public StorageFactory(IConfiguration configuration, MsSqlServerFactory msSqlServerFactory)
    {
        _configuration = configuration;
        _msSqlServerFactory = msSqlServerFactory;
    }

    public ICategoryRepository ChangeCategoryRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => new CategoryXmlRepository(),
            StorageType.MsSqlServer => new CategoryMsSqlServerRepository(_msSqlServerFactory)
        };
    }

    public IJobRepository ChangeJobRepository(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => new JobXmlRepository(),
            StorageType.MsSqlServer => new JobMsSqlServerRepository(_msSqlServerFactory)
        };
    }
}