using Dapper;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.DAL.Implementations.Repositories;

public class JobRepository : IJobRepository
{
    private readonly DbContext _context;

    public JobRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<JobEntityResponse>> GetAll(bool sortByStatus = true, bool sortByDueDate = true)
    {
        var queryString =
            "SELECT Job.Id, Job.[Name], Category.[Name] AS CategoryName, Job.IsCompleted, Job.DueDate FROM Job INNER JOIN Category ON Category.Id = Job.CategoryId";

        using var connection = _context.CreateConnection();
        var jobs = await connection.QueryAsync<JobEntityResponse>(queryString);

        return jobs;
    }
}