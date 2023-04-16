namespace JustDoIt.BLL.Models.Response;

public class JobModelResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public bool IsCompleted { get; set; }
}