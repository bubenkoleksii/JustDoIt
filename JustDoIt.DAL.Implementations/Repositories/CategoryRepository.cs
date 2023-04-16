using Dapper;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.DAL.Implementations.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbContext _context;

    public CategoryRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryEntityResponse>> GetAll()
    {
        var queryString = "SELECT Category.Id, Category.[Name], COUNT(Job.CategoryId) AS CountOfJobs FROM Category " +
                          "LEFT JOIN Job ON Category.Id = Job.CategoryId " +
                          "GROUP BY Category.Id, Category.[Name] " +
                          "ORDER BY CountOfJobs DESC";

        using var connection = _context.CreateConnection();
        var categories = await connection.QueryAsync<CategoryEntityResponse>(queryString);

        return categories;
    }

    public async Task Add(CategoryEntityRequest category)
    {
        var queryString = $"INSERT INTO Category ([Name]) VALUES(@{nameof(category.Name)})";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(queryString, category);
    }
}