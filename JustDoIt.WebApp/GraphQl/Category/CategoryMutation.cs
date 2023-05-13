using GraphQL;
using GraphQL.Types;

namespace JustDoIt.WebApp.GraphQl.Category;

public class CategoryMutation : ObjectGraphType
{
    public CategoryMutation()
    {
        Field<StringGraphType>("hello")
            .Argument<StringGraphType>("id")
            .Resolve(context =>
            {
                var id = context.GetArgument<string>("id");
                return $"Hello ${id}";
            });
    }
}