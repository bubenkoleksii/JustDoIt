using System.ComponentModel.DataAnnotations;

namespace JustDoIt.WebApp.Models.Request;

public class JobRequest
{
    [Required]
    [Display(Name = "Category")]
    public Guid CategoryId { get; set; }

    [Required] 
    [MaxLength(100)] 
    public string Name { get; set; } = null!;

    [Required]
    [Display(Name = "Due date")]
    public DateTime DueDate { get; set; } = DateTime.Today.AddDays(1);

    [Display(Name = "Status")] 
    public bool IsCompleted { get; set; }
}