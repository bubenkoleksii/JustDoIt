namespace JustDoIt.DAL.Entities.Response;

public class CategoryEntityResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountOfJobs { get; set; }
}