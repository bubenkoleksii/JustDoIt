using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.Shared;

namespace JustDoIt.BLL.Interfaces;

public interface ICategoryService
{
    public Task<ICollection<CategoryModelResponse>> GetAll(RepositoryType repositoryType);

    public Task Add(CategoryModelRequest category, RepositoryType repositoryType);

    public Task Remove(Guid id, RepositoryType repositoryType);
}