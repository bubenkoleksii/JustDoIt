using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;

namespace JustDoIt.DAL.Interfaces;

public interface ICategoryRepository
{
    public Task<IEnumerable<CategoryEntityResponse>> GetAll();

    public Task Add(CategoryEntityRequest category);
}