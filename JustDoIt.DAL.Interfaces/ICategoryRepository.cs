using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;

namespace JustDoIt.DAL.Interfaces;

public interface ICategoryRepository
{
    public Task<IEnumerable<CategoryEntityResponse>> GetAll();

    public Task<CategoryEntityResponse> GetOneById(Guid id);

    public Task<CategoryEntityResponse> GetOneByName(string name);

    public Task Add(CategoryEntityRequest category);

    public Task Remove(Guid id);
}