using Dapper;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.DAL.Implementations.Repositories;

public class JobRepository : IJobRepository
{
    private readonly DbFactory _factory;

    public JobRepository(DbFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<JobEntityResponse>> GetAll(bool sortByDueDate = true)
    {
        var queryString =
            "SELECT Job.Id, Job.[Name], Category.[Name] AS CategoryName, Job.IsCompleted, Job.DueDate, " +
            " ABS(DATEDIFF(MINUTE, GETDATE(), Job.DueDate)) AS DateDifferenceInMinutes " +
            "FROM Job INNER JOIN Category ON Category.Id = Job.CategoryId ";

        queryString += sortByDueDate ? "ORDER BY Job.IsCompleted, DateDifferenceInMinutes" : "ORDER BY Job.IsCompleted";

        using var connection = _factory.CreateConnection();
        var jobs = await connection.QueryAsync<JobEntityResponse>(queryString);

        return jobs;
    }

    public async Task<IEnumerable<JobEntityResponse>> GetByCategory(Guid categoryId, bool sortByDueDate = true)
    {
        var queryString =
            "SELECT Job.Id, Job.[Name], Category.[Name] AS CategoryName, Job.IsCompleted, Job.DueDate, " +
            " ABS(DATEDIFF(MINUTE, GETDATE(), Job.DueDate)) AS DateDifferenceInMinutes " +
            "FROM Job INNER JOIN Category ON Category.Id = Job.CategoryId " +
            $"WHERE CategoryId = @{nameof(categoryId)} ";

        queryString += sortByDueDate ? "ORDER BY Job.IsCompleted, DateDifferenceInMinutes" : "ORDER BY Job.IsCompleted";

        using var connection = _factory.CreateConnection();
        var jobs = await connection.QueryAsync<JobEntityResponse>(queryString, new { categoryId });

        return jobs;
    }

    public async Task<JobEntityResponse> GetOneById(Guid id)
    {
        var queryString = $"SELECT * FROM Job WHERE Id = @{nameof(id)}";

        using var connection = _factory.CreateConnection();
        var job = await connection.QueryFirstOrDefaultAsync<JobEntityResponse>(queryString, new { id });

        return job;
    }

    public async Task Add(JobEntityRequest job)
    {
        var queryString =
            "INSERT INTO Job (CategoryId, [Name], DueDate, IsCompleted) " +
            $"VALUES (@{nameof(job.CategoryId)}, @{nameof(job.Name)}, @{nameof(job.DueDate)}, @{nameof(job.IsCompleted)})";

        using var connection = _factory.CreateConnection();
        await connection.ExecuteAsync(queryString, job);
    }

    public async Task Remove(Guid id)
    {
        var queryString = $"DELETE FROM Job WHERE Id = @{nameof(id)}";

        using var connection = _factory.CreateConnection();
        await connection.ExecuteAsync(queryString, new { id });
    }

    public async Task Check(Guid id)
    {
        var queryString = $"UPDATE Job SET IsCompleted = 1 WHERE Id = @{nameof(id)}";

        using var connection = _factory.CreateConnection();
        await connection.ExecuteAsync(queryString, new { id });
    }

    public async Task Uncheck(Guid id)
    {
        var queryString = $"UPDATE Job SET IsCompleted = 0 WHERE Id = @{nameof(id)}";

        using var connection = _factory.CreateConnection();
        await connection.ExecuteAsync(queryString, new { id });
    }
}