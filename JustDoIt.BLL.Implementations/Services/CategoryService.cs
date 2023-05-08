using AutoMapper;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;

namespace JustDoIt.BLL.Implementations.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;

    private readonly IRepositoryFactory _repositoryFactory;

    private ICategoryRepository _categoryRepository;

    public CategoryService(IRepositoryFactory repositoryFactory, IMapper mapper)
    {
        _repositoryFactory = repositoryFactory;
        _mapper = mapper;
    }

    public async Task<ICollection<CategoryModelResponse>> GetAll(RepositoryType repositoryType)
    {
        _categoryRepository = _repositoryFactory.GetCategoryRepository(repositoryType);

        var categories = await _categoryRepository.GetAll();

        var categoriesResponse = _mapper.Map<IEnumerable<CategoryModelResponse>>(categories);
        return categoriesResponse.ToList();
    }

    public async Task Add(CategoryModelRequest category, RepositoryType repositoryType)
    {
        _categoryRepository = _repositoryFactory.GetCategoryRepository(repositoryType);

        var categoryRequest = _mapper.Map<CategoryEntityRequest>(category);

        var existingCategory = await _categoryRepository.GetOneByName(categoryRequest.Name);
        if (existingCategory != null)
            throw new ArgumentException("The category was not added because a category with that name already exists.");

        await _categoryRepository.Add(categoryRequest);
    }

    public async Task Remove(Guid id, RepositoryType repositoryType)
    {
        _categoryRepository = _repositoryFactory.GetCategoryRepository(repositoryType);

        var existingCategory = await _categoryRepository.GetOneById(id);
        if (existingCategory != null)
            await _categoryRepository.Remove(id);
    }
}