using GraphQL.Types;

namespace JustDoIt.WebApp.GraphQl.Job;

public class JobSchema : Schema
{
    public JobSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<JobQuery>();
        Mutation = serviceProvider.GetRequiredService<JobMutation>();
    }
}