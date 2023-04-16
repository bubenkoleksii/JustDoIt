namespace JustDoIt.BLL.Models.Response;

public class CategoryModelResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountOfJobs { get; set; }
}