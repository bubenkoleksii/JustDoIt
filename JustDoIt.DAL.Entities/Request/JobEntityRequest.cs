namespace JustDoIt.DAL.Entities.Request;

public class JobEntityRequest
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public bool IsCompleted { get; set; }
}