using JustDoIt.Shared;

namespace JustDoIt.DAL.Interfaces;

public interface IStorageFactory
{
    public ICategoryRepository GetCategoryRepository(StorageType storageType);

    public IJobRepository GetJobRepository(StorageType storageType);
}