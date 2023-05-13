using GraphQL.Types;
using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.GraphQl.Category.Types;

public class CategoryResponseType : ObjectGraphType<CategoryResponse>
{
    public CategoryResponseType()
    {
        Name = nameof(CategoryResponse);

        Field(i => i.Id, nullable: false, type: typeof(IdGraphType))
            .Description("Id field for category object");
        Field(i => i.Name, nullable: false)
            .Description("Name field for category object");
        Field(i => i.CountOfJobs, nullable: false, type: typeof(IntGraphType))
            .Description("Count of jobs field for category object");
    }
}