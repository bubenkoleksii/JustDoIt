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
    private readonly Func<StorageType, ICategoryRepository> _categoryRepositoryFactory;
    private readonly IMapper _mapper;

    public CategoryService(Func<StorageType, ICategoryRepository> categoryRepositoryFactory, IMapper mapper)
    {
        _categoryRepositoryFactory = categoryRepositoryFactory;
        _mapper = mapper;
    }

    public async Task<ICollection<CategoryModelResponse>> GetAll(StorageType storageType)
    {
        var categoryRepository = _categoryRepositoryFactory(storageType);

        var categories = await categoryRepository.GetAll();

        var categoriesResponse = _mapper.Map<IEnumerable<CategoryModelResponse>>(categories);
        return categoriesResponse.ToList();
    }

    public async Task Add(CategoryModelRequest category, StorageType storageType)
    {
        var categoryRepository = _categoryRepositoryFactory(storageType);

        var categoryRequest = _mapper.Map<CategoryEntityRequest>(category);

        var existingCategory = await categoryRepository.GetOneByName(categoryRequest.Name);
        if (existingCategory != null)
            throw new ArgumentException("The category was not added because a category with that name already exists.");

        await categoryRepository.Add(categoryRequest);
    }

    public async Task Remove(Guid id, StorageType storageType)
    {
        var categoryRepository = _categoryRepositoryFactory(storageType);

        var existingCategory = await categoryRepository.GetOneById(id);
        if (existingCategory != null)
            await categoryRepository.Remove(id);
    }
}