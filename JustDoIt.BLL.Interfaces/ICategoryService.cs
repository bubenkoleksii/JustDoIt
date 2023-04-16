using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;

namespace JustDoIt.BLL.Interfaces;

public interface ICategoryService
{
    public Task<ICollection<CategoryModelResponse>> GetAll();

    public Task Add(CategoryModelRequest category);

    public Task Remove(Guid id);
}