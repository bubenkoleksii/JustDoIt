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

    public ICategoryRepository GetCategoryRepository(RepositoryType repositoryType)
    {
        return repositoryType switch
        {
            RepositoryType.Xml => new CategoryXmlRepository(_xmlConnectionFactory),
            RepositoryType.MsSqlServer => new CategoryMsSqlServerRepository(_msSqlServerConnectionFactory)
        };
    }

    public IJobRepository GetJobRepository(RepositoryType repositoryType)
    {
        return repositoryType switch
        {
            RepositoryType.Xml => new JobXmlRepository(_xmlConnectionFactory),
            RepositoryType.MsSqlServer => new JobMsSqlServerRepository(_msSqlServerConnectionFactory)
        };
    }
}