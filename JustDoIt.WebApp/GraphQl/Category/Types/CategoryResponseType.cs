using GraphQL.Types;
using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.GraphQl.Category.Types;

public class CategoryResponseType : ObjectGraphType<CategoryResponse>
{
    public CategoryResponseType()
    {
        Name = nameof(CategoryResponse);

        Field(i => i.Id, false, typeof(IdGraphType))
            .Description("Id field for category object");
        Field(i => i.Name, false)
            .Description("Name field for category object");
        Field(i => i.CountOfJobs, false, typeof(IntGraphType))
            .Description("Count of jobs field for category object");
    }
}