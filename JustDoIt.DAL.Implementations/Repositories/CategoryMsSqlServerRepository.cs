using Dapper;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.DAL.Implementations.Repositories;

public class CategoryMsSqlServerRepository : ICategoryRepository
{
    private readonly MsSqlServerFactory _factory;

    public CategoryMsSqlServerRepository(MsSqlServerFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<CategoryEntityResponse>> GetAll()
    {
        var queryString = "SELECT Category.Id, Category.[Name], COUNT(Job.CategoryId) AS CountOfJobs FROM Category " +
                          "LEFT JOIN Job ON Category.Id = Job.CategoryId " +
                          "GROUP BY Category.Id, Category.[Name] " +
                          "ORDER BY CountOfJobs DESC";

        using var connection = _factory.CreateConnection();
        var categories = await connection.QueryAsync<CategoryEntityResponse>(queryString);

        return categories;
    }

    public async Task<CategoryEntityResponse> GetOneById(Guid id)
    {
        var queryString = $"SELECT * FROM Category WHERE Id = @{nameof(id)}";

        using var connection = _factory.CreateConnection();
        var category = await connection.QueryFirstOrDefaultAsync<CategoryEntityResponse>(queryString, new { id });

        return category;
    }

    public async Task<CategoryEntityResponse> GetOneByName(string name)
    {
        var queryString = $"SELECT * FROM Category WHERE [Name] = @{nameof(name)}";

        using var connection = _factory.CreateConnection();
        var category = await connection.QueryFirstOrDefaultAsync<CategoryEntityResponse>(queryString, new { name });

        return category;
    }

    public async Task Add(CategoryEntityRequest category)
    {
        var queryString = $"INSERT INTO Category ([Name]) VALUES(@{nameof(category.Name)})";

        using var connection = _factory.CreateConnection();
        await connection.ExecuteAsync(queryString, category);
    }

    public async Task Remove(Guid id)
    {
        var queryString = $"DELETE FROM Category WHERE Id = @{nameof(id)}";

        using var connection = _factory.CreateConnection();
        await connection.ExecuteAsync(queryString, new { id });
    }
}