using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;

namespace JustDoIt.DAL.Interfaces;

public interface IJobRepository
{
    public Task<IEnumerable<JobEntityResponse>> GetAll(bool sortByDueDate = true);

    public Task Add(JobEntityRequest job);
}