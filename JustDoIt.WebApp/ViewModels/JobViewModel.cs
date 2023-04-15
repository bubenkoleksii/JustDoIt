using JustDoIt.WebApp.Models.Request;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JustDoIt.WebApp.ViewModels
{
    public class JobViewModel
    {
        public JobRequest Job { get; set; } = null!;

        public IEnumerable<SelectListItem> CategoryMap = null!;
    }
}
