using Dapper;
using JustDoIt.DAL.Entities.Request;
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

    public async Task<IEnumerable<JobEntityResponse>> GetAll(bool sortByDueDate = true)
    {
        var queryString =
            "SELECT Job.Id, Job.[Name], Category.[Name] AS CategoryName, Job.IsCompleted, Job.DueDate, " +
            " ABS(DATEDIFF(MINUTE, GETDATE(), Job.DueDate)) AS DateDifferenceInMinutes " +
            "FROM Job INNER JOIN Category ON Category.Id = Job.CategoryId ";

        queryString += sortByDueDate ? "ORDER BY Job.IsCompleted, DateDifferenceInMinutes" : "ORDER BY Job.IsCompleted";

        using var connection = _context.CreateConnection();
        var jobs = await connection.QueryAsync<JobEntityResponse>(queryString);

        return jobs;
    }

    public async Task Add(JobEntityRequest job)
    {
        var queryString =
            "INSERT INTO Job (CategoryId, [Name], DueDate, IsCompleted) " +
            $"VALUES (@{nameof(job.CategoryId)}, @{nameof(job.Name)}, @{nameof(job.DueDate)}, @{nameof(job.IsCompleted)})";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(queryString, job);
    }
}