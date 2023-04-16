using System.Diagnostics;
using AutoMapper;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Request;
using JustDoIt.WebApp.Models.Request;
using JustDoIt.WebApp.Models.Response;
using JustDoIt.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.WebApp.Controllers;

public class IndexController : Controller
{
    private readonly IJobService _jobService;

    private readonly ICategoryService _categoryService;

    private readonly IMapper _mapper;

    public IndexController(IJobService jobService, ICategoryService categoryService, IMapper mapper)
    {
        _jobService = jobService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var indexViewModel = await GetCategoriesAndJobs();
            return View(indexViewModel);
        }
        catch
        {
            return View("~/Views/Shared/InternalError.cshtml");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddJob(JobRequest job)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The job was not added. Reopen the modal window with the adding job for details.";

                var indexViewModel = await GetCategoriesAndJobs();
                return View(nameof(Index), indexViewModel);
            }

            var jobModelRequest = _mapper.Map<JobModelRequest>(job);
            await _jobService.Add(jobModelRequest);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View("~/Views/Shared/InternalError.cshtml");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCategory(CategoryRequest category)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The category was not added. Reopen the modal window with the adding category for details.";

                var indexViewModel = await GetCategoriesAndJobs();
                return View(nameof(Index), indexViewModel);
            }

            var categoryRequest = _mapper.Map<CategoryModelRequest>(category);
            await _categoryService.Add(categoryRequest);
            
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View("~/Views/Shared/InternalError.cshtml");
        }
    }

    private async Task<IndexViewModel> GetCategoriesAndJobs()
    {
        var jobs = await _jobService.GetAll();
        var jobsResponse = _mapper.Map<ICollection<JobResponse>>(jobs);

        var categories = await _categoryService.GetAll();
        var categoriesResponse = _mapper.Map<ICollection<CategoryResponse>>(categories);

        var indexViewModel = new IndexViewModel
        {
            Jobs = jobsResponse,
            Categories = categoriesResponse
        };

        return indexViewModel;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}