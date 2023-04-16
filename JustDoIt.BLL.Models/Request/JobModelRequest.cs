namespace JustDoIt.BLL.Models.Request;

public class JobModelRequest
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public bool IsCompleted { get; set; }
}