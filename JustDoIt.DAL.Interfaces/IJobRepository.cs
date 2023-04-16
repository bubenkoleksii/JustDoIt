using JustDoIt.DAL.Entities.Response;

namespace JustDoIt.DAL.Interfaces;

public interface IJobRepository
{
    public Task<IEnumerable<JobEntityResponse>> GetAll(bool sortByStatus = true, bool sortByDueDate = true);
}