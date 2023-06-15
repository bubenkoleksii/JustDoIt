using GraphQL.Types;

namespace JustDoIt.WebApp.GraphQl.Job;

public class RootSchema : Schema
{
    public RootSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<RootQuery>();
        Mutation = serviceProvider.GetRequiredService<RootMutation>();
    }
}