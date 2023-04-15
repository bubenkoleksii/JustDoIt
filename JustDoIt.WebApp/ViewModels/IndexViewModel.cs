using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.ViewModels;

public class IndexViewModel
{
    public ICollection<JobResponse>? Jobs { get; set; }

    public ICollection<CategoryResponse>? Categories { get; set; }
}