using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.Shared;

namespace JustDoIt.BLL.Interfaces;

public interface IJobService
{
    public Task<ICollection<JobModelResponse>> GetAll(RepositoryType repositoryType, bool sortByDueDate = true);

    public Task<ICollection<JobModelResponse>> GetByCategory(Guid categoryId, RepositoryType repositoryType, bool sortByDueDate = true);

    public Task Add(JobModelRequest job, RepositoryType repositoryType);

    public Task Remove(Guid id, RepositoryType repositoryType);

    public Task Check(Guid id, RepositoryType repositoryType);
}