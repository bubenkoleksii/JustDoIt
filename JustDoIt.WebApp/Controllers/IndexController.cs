using System.Diagnostics;
using AutoMapper;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Request;
using JustDoIt.Shared;
using JustDoIt.WebApp.Models.Request;
using JustDoIt.WebApp.Models.Response;
using JustDoIt.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.WebApp.Controllers;

public class IndexController : Controller
{
    private readonly ICategoryService _categoryService;

    private readonly IJobService _jobService;

    private readonly IMapper _mapper;

    public IndexController(IJobService jobService, ICategoryService categoryService, IMapper mapper)
    {
        _jobService = jobService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var storageType = Request.Cookies["Storage"];

            if (storageType == null)
            {
                storageType = StorageType.MsSqlServer.ToString();

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                };

                Response.Cookies.Append("Storage", storageType, cookieOptions);
            }

            var indexViewModel = await GetAllCategoriesAndJobs(storageType);
            return View(indexViewModel);
        }
        catch
        {
            return View("~/Views/Shared/InternalError.cshtml");
        }
    }

    [HttpGet]
    public async Task<IActionResult> ChangeStorage(string storageType)
    {
        try
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };

            Response.Cookies.Append("Storage", storageType, cookieOptions);

            var indexViewModel = await GetAllCategoriesAndJobs(storageType);
            return View(nameof(Index), indexViewModel);
        }
        catch
        {
            return View("~/Views/Shared/InternalError.cshtml");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetJobsByCategory(Guid id)
    {
        var storageType = Request.Cookies["Storage"];

        try
        {
            var indexViewModel = await GetCategoriesAndJobsByCategory(id, storageType);
            return View(nameof(Index), indexViewModel);
        }
        catch (ArgumentNullException exception)
        {
            TempData["Error"] = exception.Message;

            var indexViewModel = await GetAllCategoriesAndJobs(storageType);
            return View(nameof(Index), indexViewModel);
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
        var storageType = Request.Cookies["Storage"];

        try
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "The job was not added. Reopen the modal window with the adding job for details.";

                var indexViewModel = await GetAllCategoriesAndJobs(storageType);
                return View(nameof(Index), indexViewModel);
            }

            var jobModelRequest = _mapper.Map<JobModelRequest>(job);
            await _jobService.Add(jobModelRequest, XmlStorageHelper.GetStorageTypeByString(storageType));

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View("~/Views/Shared/InternalError.cshtml");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveJob(Guid id, Guid categoryId, bool isSingleCategoryView)
    {
        var storageType = Request.Cookies["Storage"];

        try
        {
            await _jobService.Remove(id, XmlStorageHelper.GetStorageTypeByString(storageType));

            if (!isSingleCategoryView)
                return RedirectToAction(nameof(Index));

            var indexViewModel = await GetCategoriesAndJobsByCategory(categoryId, storageType);
            return View(nameof(Index), indexViewModel);
        }
        catch
        {
            return View("~/Views/Shared/InternalError.cshtml");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckJob(Guid id, Guid categoryId, bool isSingleCategoryView)
    {
        var storageType = Request.Cookies["Storage"];

        try
        {
            await _jobService.Check(id, XmlStorageHelper.GetStorageTypeByString(storageType));

            if (!isSingleCategoryView)
                return RedirectToAction(nameof(Index));

            var indexViewModel = await GetCategoriesAndJobsByCategory(categoryId, storageType);
            return View(nameof(Index), indexViewModel);
        }
        catch (ArgumentNullException exception)
        {
            TempData["Error"] = exception.Message;

            var indexViewModel = await GetAllCategoriesAndJobs(storageType);
            return View(nameof(Index), indexViewModel);
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
        var storageType = Request.Cookies["Storage"];

        try
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] =
                    "The category was not added. Reopen the modal window with the adding category for details.";

                var indexViewModel = await GetAllCategoriesAndJobs(storageType);
                return View(nameof(Index), indexViewModel);
            }

            var categoryRequest = _mapper.Map<CategoryModelRequest>(category);
            await _categoryService.Add(categoryRequest, XmlStorageHelper.GetStorageTypeByString(storageType));

            return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException exception)
        {
            TempData["Error"] = exception.Message;

            var indexViewModel = await GetAllCategoriesAndJobs(storageType);
            return View(nameof(Index), indexViewModel);
        }
        catch
        {
            return View("~/Views/Shared/InternalError.cshtml");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveCategory(Guid id)
    {
        var storageType = Request.Cookies["Storage"];

        try
        {
            await _categoryService.Remove(id, XmlStorageHelper.GetStorageTypeByString(storageType));

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View("~/Views/Shared/InternalError.cshtml");
        }
    }

    private async Task<IndexViewModel> GetAllCategoriesAndJobs(string storageType)
    {
        var jobs = await _jobService.GetAll(XmlStorageHelper.GetStorageTypeByString(storageType));
        var jobsResponse = _mapper.Map<ICollection<JobResponse>>(jobs);

        var categories = await _categoryService.GetAll(XmlStorageHelper.GetStorageTypeByString(storageType));
        var categoriesResponse = _mapper.Map<ICollection<CategoryResponse>>(categories);

        var storageTypesDictionary = GetStorageTypesDictionary(storageType);

        var indexViewModel = new IndexViewModel
        {
            Jobs = jobsResponse,
            Categories = categoriesResponse,
            StorageTypes = storageTypesDictionary
        };

        return indexViewModel;
    }

    private async Task<IndexViewModel> GetCategoriesAndJobsByCategory(Guid categoryId, string storageType)
    {
        var jobs = await _jobService.GetByCategory(categoryId, XmlStorageHelper.GetStorageTypeByString(storageType));
        var jobsResponse = _mapper.Map<ICollection<JobResponse>>(jobs);

        var categories = await _categoryService.GetAll(XmlStorageHelper.GetStorageTypeByString(storageType));
        var categoriesResponse = _mapper.Map<ICollection<CategoryResponse>>(categories);

        var storageTypesDictionary = GetStorageTypesDictionary(storageType);

        var indexViewModel = new IndexViewModel
        {
            Jobs = jobsResponse,
            Categories = categoriesResponse,
            IsSingleCategoryView = true,
            StorageTypes = storageTypesDictionary
        };

        return indexViewModel;
    }

    private Dictionary<string, bool> GetStorageTypesDictionary(string selectedStorageType)
    {
        var storageTypesDictionary = Enum.GetValues<StorageType>()
            .ToDictionary(storageType => storageType.ToString(),
                storageType => storageType.ToString() == selectedStorageType);

        return storageTypesDictionary;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}