using GraphQL.Types;
using JustDoIt.WebApp.GraphQl.Category;
using JustDoIt.WebApp.GraphQl.Job;

namespace JustDoIt.WebApp.GraphQl;

public class RootMutation : ObjectGraphType
{
    public RootMutation()
    {
        Field<CategoryMutation>("category").Resolve(_ => new { });
        Field<JobMutation>("job").Resolve(_ => new { });
    }
}