namespace JustDoIt.WebApp.Models.Response;

public class CategoryResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountOfJobs { get; set; }
}