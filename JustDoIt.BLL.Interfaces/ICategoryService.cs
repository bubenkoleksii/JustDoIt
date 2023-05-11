using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.Shared;

namespace JustDoIt.BLL.Interfaces;

public interface ICategoryService
{
    public Task<ICollection<CategoryModelResponse>> GetAll(StorageType storageType);

    public Task Add(CategoryModelRequest category, StorageType storageType);

    public Task Remove(Guid id, StorageType storageType);
}