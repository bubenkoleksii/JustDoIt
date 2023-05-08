using JustDoIt.Shared;

namespace JustDoIt.DAL.Interfaces;

public interface IRepositoryFactory
{
    public ICategoryRepository GetCategoryRepository(RepositoryType repositoryType);

    public IJobRepository GetJobRepository(RepositoryType repositoryType);
}