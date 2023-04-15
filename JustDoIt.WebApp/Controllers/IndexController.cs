using System.Diagnostics;
using JustDoIt.WebApp.Models.Request;
using JustDoIt.WebApp.Models.Response;
using JustDoIt.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.WebApp.Controllers;

public class IndexController : Controller
{
    private readonly ILogger<IndexController> _logger;

    public IndexController(ILogger<IndexController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var job1 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Change life",
            CategoryName = "Family",
            DueDate = new DateTime(),
            IsCompleted = false
        };

        var job2 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Play football",
            CategoryName = "Work",
            DueDate = new DateTime(),
            IsCompleted = true
        };

        var job3 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Play football",
            CategoryName = "Work",
            DueDate = new DateTime(),
            IsCompleted = true
        };

        var job4 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Play football",
            CategoryName = "Work",
            DueDate = new DateTime(),
            IsCompleted = true
        };

        var job5 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Play football",
            CategoryName = "Work",
            DueDate = new DateTime(),
            IsCompleted = true
        };

        var jobsList = new List<JobResponse>
        {
            job1,
            job2,
            job3,
            job4,
            job5
        };

        var category1 = new CategoryResponse
        {
            Id = Guid.NewGuid(),
            Name = "Family",
            CountOfJobs = 5
        };

        var categories = new List<CategoryResponse>
        {
            category1
        };

        var indexViewModel = new IndexViewModel
        {
            Jobs = jobsList,
            Categories = categories
        };

        return View(indexViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddJob(JobRequest job)
    {
        var job1 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Change life",
            CategoryName = "Family",
            DueDate = new DateTime(),
            IsCompleted = false
        };

        var job2 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Play football",
            CategoryName = "Work",
            DueDate = new DateTime(),
            IsCompleted = true
        };

        var job3 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Play football",
            CategoryName = "Work",
            DueDate = new DateTime(),
            IsCompleted = true
        };

        var job4 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Play football",
            CategoryName = "Work",
            DueDate = new DateTime(),
            IsCompleted = true
        };

        var job5 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Play football",
            CategoryName = "Work",
            DueDate = new DateTime(),
            IsCompleted = true
        };

        var jobsList = new List<JobResponse>
        {
            job1,
            job2,
            job3,
            job4,
            job5
        };

        var category1 = new CategoryResponse
        {
            Id = Guid.NewGuid(),
            Name = "Family",
            CountOfJobs = 5
        };

        var categories = new List<CategoryResponse>
        {
            category1
        };

        var indexViewModel = new IndexViewModel
        {
            Jobs = jobsList,
            Categories = categories
        };

        if (!ModelState.IsValid)
        {
            TempData["Error"] = "The job was not added. Reopen the modal window with the added job for details.";
            return View(nameof(Index), indexViewModel);
        }

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}