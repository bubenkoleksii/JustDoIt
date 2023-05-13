using AutoMapper;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using JustDoIt.BLL.Interfaces;
using JustDoIt.Shared;
using JustDoIt.WebApp.GraphQl.Category.Types;
using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.GraphQl.Category;

public class CategoryQuery : ObjectGraphType
{
    public CategoryQuery(IMapper  mapper) {

        Field<ListGraphType<CategoryResponseType>>("getAll")
            .Resolve()
            .WithScope()
            .WithService<ICategoryService>()
            .ResolveAsync(async (context, service) =>
            {
                var categories = await service.GetAll(StorageType.Xml);

                var categoriesResponse = mapper.Map<ICollection<CategoryResponse>>(categories);
                return categoriesResponse;
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