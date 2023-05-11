using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.Shared;

namespace JustDoIt.BLL.Interfaces;

public interface IJobService
{
    public Task<ICollection<JobModelResponse>> GetAll(StorageType storageType, bool sortByDueDate = true);

    public Task<ICollection<JobModelResponse>> GetByCategory(Guid categoryId, StorageType storageType,
        bool sortByDueDate = true);

    public Task Add(JobModelRequest job, StorageType storageType);

    public Task Remove(Guid id, StorageType storageType);

    public Task Check(Guid id, StorageType storageType);
}