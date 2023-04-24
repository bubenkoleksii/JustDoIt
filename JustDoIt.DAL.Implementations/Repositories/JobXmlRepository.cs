using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.DAL.Implementations.Repositories;

public class JobXmlRepository : IJobRepository
{
    public Task<IEnumerable<JobEntityResponse>> GetAll(bool sortByDueDate = true)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<JobEntityResponse>> GetByCategory(Guid categoryId, bool sortByDueDate = true)
    {
        throw new NotImplementedException();
    }

    public Task<JobEntityResponse> GetOneById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Add(JobEntityRequest job)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Check(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Uncheck(Guid id)
    {
        throw new NotImplementedException();
    }
}