using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.ViewModels.Job;

public class JobViewModelResponse
{
    public IEnumerable<JobResponse>? Jobs { get; set; }

    public bool IsSingleCategoryView { get; set; } = false;
}
