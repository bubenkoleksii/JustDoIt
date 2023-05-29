using AutoMapper;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;

namespace JustDoIt.BLL.Implementations.Services;

public class JobService : IJobService
{
    private readonly Func<StorageType, ICategoryRepository> _categoryRepositoryFactory;

    private readonly Func<StorageType, IJobRepository> _jobRepositoryFactory;
    private readonly IMapper _mapper;

    public JobService(Func<StorageType, IJobRepository> jobRepositoryFactory,
        Func<StorageType, ICategoryRepository> categoryRepositoryFactory, IMapper mapper)
    {
        _jobRepositoryFactory = jobRepositoryFactory;
        _categoryRepositoryFactory = categoryRepositoryFactory;
        _mapper = mapper;
    }

    public async Task<ICollection<JobModelResponse>> GetAll(StorageType storageType, bool sortByDueDate = true)
    {
        var jobRepository = _jobRepositoryFactory(storageType);

        var jobs = await jobRepository.GetAll();

        var jobsResponse = _mapper.Map<IEnumerable<JobModelResponse>>(jobs);
        return jobsResponse.ToList();
    }

    public async Task<ICollection<JobModelResponse>> GetByCategory(Guid categoryId, StorageType storageType,
        bool sortByDueDate = true)
    {
        var jobRepository = _jobRepositoryFactory(storageType);
        var categoryRepository = _categoryRepositoryFactory(storageType);

        var existingCategory = await categoryRepository.GetOneById(categoryId);
        if (existingCategory == null)
            throw new ArgumentNullException("The jobs was not selected because category not found.");

        var jobs = await jobRepository.GetByCategory(categoryId);

        var jobsResponse = _mapper.Map<IEnumerable<JobModelResponse>>(jobs);
        return jobsResponse.ToList();
    }

    public async Task Add(JobModelRequest job, StorageType storageType)
    {
        var jobRepository = _jobRepositoryFactory(storageType);

        var jobRequest = _mapper.Map<JobEntityRequest>(job);
        await jobRepository.Add(jobRequest);
    }

    public async Task Remove(Guid id, StorageType storageType)
    {
        var jobRepository = _jobRepositoryFactory(storageType);

        var existingJob = await jobRepository.GetOneById(id);

        if (existingJob != null)
            await jobRepository.Remove(id);
    }

    public async Task Check(Guid id, StorageType storageType)
    {
        var jobRepository = _jobRepositoryFactory(storageType);

        var existingJob = await jobRepository.GetOneById(id);
        if (existingJob == null)
            throw new ArgumentNullException("The job was not found and its status cannot be changed.");

        if (existingJob.IsCompleted)
            await jobRepository.Uncheck(id);
        else
            await jobRepository.Check(id);
    }
}