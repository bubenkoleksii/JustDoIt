using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.DAL.Implementations.Repositories;

public class CategoryXmlRepository : ICategoryRepository
{
    public Task<IEnumerable<CategoryEntityResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<CategoryEntityResponse> GetOneById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryEntityResponse> GetOneByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task Add(CategoryEntityRequest category)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Guid id)
    {
        throw new NotImplementedException();
    }
}