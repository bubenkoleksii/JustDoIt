using System.Diagnostics;
using JustDoIt.WebApp.Models;
using JustDoIt.WebApp.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetJobs()
    {
        var job1 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Change life",
            CategoryId = Guid.NewGuid(),
            DueDate = new DateTime(),
            IsCompleted = false
        };

        var job2 = new JobResponse
        {
            Id = Guid.NewGuid(),
            Name = "Play football",
            CategoryId = Guid.NewGuid(),
            DueDate = new DateTime(),
            IsCompleted = true
        };

        var jobsList = new List<JobResponse>
        {
            job1,
            job2
        };

        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}