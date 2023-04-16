using AutoMapper;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.BLL.Implementations.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<CategoryModelResponse>> GetAll()
    {
        var categories = await _categoryRepository.GetAll();

        var categoriesResponse = _mapper.Map<IEnumerable<CategoryModelResponse>>(categories);
        return categoriesResponse.ToList();
    }

    public async Task Add(CategoryModelRequest category)
    {
        var categoryRequest = _mapper.Map<CategoryEntityRequest>(category);

        var existingCategory = await _categoryRepository.GetOneByName(categoryRequest.Name);
        if (existingCategory != null)
            throw new ArgumentException();

        await _categoryRepository.Add(categoryRequest);
    }

    public async Task Remove(Guid id)
    {
        var existingCategory = _categoryRepository.GetOneById(id);
        if (existingCategory != null)
        {
            await _categoryRepository.Remove(id);
        }
    }
}