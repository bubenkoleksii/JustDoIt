using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;

namespace JustDoIt.DAL.Interfaces;

public interface IJobRepository
{
    public Task<IEnumerable<JobEntityResponse>> GetAll(bool sortByDueDate = true);

    public Task<JobEntityResponse> GetOneById(Guid id);

    public Task Add(JobEntityRequest job);

    public Task Remove(Guid id);

    public Task Check(Guid id);
    
    public Task Uncheck(Guid id);
}