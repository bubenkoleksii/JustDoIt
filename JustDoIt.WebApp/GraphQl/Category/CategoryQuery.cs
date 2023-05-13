using GraphQL;
using GraphQL.Types;
using JustDoIt.BLL.Interfaces;
using JustDoIt.Shared;
using JustDoIt.WebApp.GraphQl.Category.Types;
using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.GraphQl.Category;

public class CategoryQuery : ObjectGraphType
{
    private readonly ICategoryService _categoryService;

    public CategoryQuery(IServiceProvider serviceProvider)
    {
        _categoryService = (ICategoryService)serviceProvider.GetService(typeof(ICategoryService));

        Field<ListGraphType<CategoryResponseType>>("getAll")
            .ResolveAsync(async _ =>
            {
                var categories = await _categoryService.GetAll(StorageType.Xml);
                return categories;
            });

        Field<CategoryResponseType>("get")
            .Resolve(context => new CategoryResponse()
            {
                Id = new Guid(),
                Name = "Cat1",
                CountOfJobs = 5
            });
    }
}