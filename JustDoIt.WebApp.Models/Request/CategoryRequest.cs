using System.ComponentModel.DataAnnotations;

namespace JustDoIt.WebApp.Models.Request;

public class CategoryRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}