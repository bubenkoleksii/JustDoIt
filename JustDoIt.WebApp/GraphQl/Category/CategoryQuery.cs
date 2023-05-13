using GraphQL;
using GraphQL.Types;
using JustDoIt.WebApp.GraphQl.Category.Types;
using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.GraphQl.Category;

public class CategoryQuery : ObjectGraphType
{
    public CategoryQuery()
    {
        Field<StringGraphType>("hello")
            .Argument<StringGraphType>("id")
            .Resolve(context =>
            { 
                var id = context.GetArgument<string>("id");
                return $"Hello ${id}";
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