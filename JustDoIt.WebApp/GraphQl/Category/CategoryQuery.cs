using AutoMapper;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using JustDoIt.BLL.Interfaces;
using JustDoIt.Shared;
using JustDoIt.WebApp.GraphQl.Category.Types;
using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.GraphQl.Category;

public class CategoryQuery : ObjectGraphType
{
    public CategoryQuery(IHttpContextAccessor contextAccessor, IMapper mapper)
    {
        Field<ListGraphType<CategoryResponseType>>("getAll")
            .Resolve()
            .WithScope()
            .WithService<ICategoryService>()
            .ResolveAsync(async (_, service) =>
            {
                var storageType =
                    StorageHelper.GetStorageTypeByString(contextAccessor.HttpContext!.Request.Headers["StorageType"]!);

                var categories = await service.GetAll(storageType);

                var categoriesResponse = mapper.Map<ICollection<CategoryResponse>>(categories);
                return categoriesResponse;
            });
    }
}