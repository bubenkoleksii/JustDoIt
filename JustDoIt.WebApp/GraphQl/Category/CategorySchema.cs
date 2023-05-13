using GraphQL.Types;

namespace JustDoIt.WebApp.GraphQl.Category;

public class CategorySchema : Schema
{
    public CategorySchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<CategoryQuery>();
        Mutation = serviceProvider.GetRequiredService<CategoryMutation>();
    }
}