using JustDoIt.DAL.Implementations.Repositories;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;

namespace JustDoIt.DAL.Implementations;

public static class RepositoryFactory
{
    public static ICategoryRepository GetCategoryRepository(IServiceProvider serviceProvider, StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => (ICategoryRepository)serviceProvider.GetService(typeof(CategoryXmlRepository))!,
            StorageType.MsSqlServer => (ICategoryRepository)serviceProvider.GetService(
                typeof(CategoryMsSqlServerRepository))!,
            _ => throw new ArgumentOutOfRangeException(nameof(storageType), storageType, null)
        };
    }

    public static IJobRepository GetJobRepository(IServiceProvider serviceProvider, StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Xml => (IJobRepository)serviceProvider.GetService(typeof(JobXmlRepository))!,
            StorageType.MsSqlServer => (IJobRepository)serviceProvider.GetService(typeof(JobMsSqlServerRepository))!,
            _ => throw new ArgumentOutOfRangeException(nameof(storageType), storageType, null)
        };
    }
}