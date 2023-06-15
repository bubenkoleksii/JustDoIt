using GraphQL.Types;
using JustDoIt.WebApp.GraphQl.Category;
using JustDoIt.WebApp.GraphQl.Job;

namespace JustDoIt.WebApp.GraphQl;

public class RootQuery : ObjectGraphType
{
    public RootQuery()
    {
        Field<CategoryQuery>("category").Resolve(_ => new { });
        Field<JobQuery>("job").Resolve(_ => new { });
    }
}