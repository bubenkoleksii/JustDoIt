using JustDoIt.Shared;

namespace JustDoIt.DAL.Interfaces;

public interface IRepositoryFactory
{
    public ICategoryRepository GetCategoryRepository(StorageType storageType);

    public IJobRepository GetJobRepository(StorageType storageType);
}