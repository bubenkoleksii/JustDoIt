using JustDoIt.Shared;

namespace JustDoIt.DAL.Interfaces;

public interface IStorageFactory
{
    public ICategoryRepository ChangeCategoryRepository(StorageType storageType);

    public IJobRepository ChangeJobRepository(StorageType storageType);
}