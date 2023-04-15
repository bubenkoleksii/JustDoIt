using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.DAL.Implementations;

public class JobRepository : IJobRepository
{
    public Task<ICollection<JobEntityResponse>> GetAll(bool sortByStatus = true, bool sortByDueDate = true)
    {
        throw new NotImplementedException();
    }
}