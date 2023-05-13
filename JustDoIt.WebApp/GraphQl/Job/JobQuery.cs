using GraphQL;
using GraphQL.Types;

namespace JustDoIt.WebApp.GraphQl.Job;

public class JobQuery : ObjectGraphType
{
    public JobQuery()
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