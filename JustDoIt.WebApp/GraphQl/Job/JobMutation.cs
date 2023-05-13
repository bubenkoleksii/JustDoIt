using GraphQL;
using GraphQL.Types;

namespace JustDoIt.WebApp.GraphQl.Job;

public class JobMutation : ObjectGraphType
{
    public JobMutation()
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