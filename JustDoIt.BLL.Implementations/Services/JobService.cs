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
    private readonly IMapper _mapper;

    private readonly IRepositoryFactory _repositoryFactory;

    private ICategoryRepository _categoryRepository;

    private  IJobRepository _jobRepository;

    public JobService(IRepositoryFactory repositoryFactory, IMapper mapper)
    {
        _repositoryFactory = repositoryFactory;
        _mapper = mapper;
    }

    public async Task<ICollection<JobModelResponse>> GetAll(RepositoryType repositoryType, bool sortByDueDate = true)
    {
        _jobRepository = _repositoryFactory.GetJobRepository(repositoryType);
        _categoryRepository = _repositoryFactory.GetCategoryRepository(repositoryType);

        var jobs = await _jobRepository.GetAll();

        var jobsResponse = _mapper.Map<IEnumerable<JobModelResponse>>(jobs);
        return jobsResponse.ToList();
    }

    public async Task<ICollection<JobModelResponse>> GetByCategory(Guid categoryId, RepositoryType repositoryType, bool sortByDueDate = true)
    {
        _categoryRepository = _repositoryFactory.GetCategoryRepository(repositoryType);
        _jobRepository = _repositoryFactory.GetJobRepository(repositoryType);

        var existingCategory = await _categoryRepository.GetOneById(categoryId);
        if (existingCategory == null)
            throw new ArgumentNullException("The jobs was not selected because category not found.");

        var jobs = await _jobRepository.GetByCategory(categoryId);

        var jobsResponse = _mapper.Map<IEnumerable<JobModelResponse>>(jobs);
        return jobsResponse.ToList();
    }

    public async Task Add(JobModelRequest job, RepositoryType repositoryType)
    {
        _jobRepository = _repositoryFactory.GetJobRepository(repositoryType);

        var jobRequest = _mapper.Map<JobEntityRequest>(job);
        await _jobRepository.Add(jobRequest);
    }

    public async Task Remove(Guid id, RepositoryType repositoryType)
    {
        _jobRepository = _repositoryFactory.GetJobRepository(repositoryType);

        var existingJob = await _jobRepository.GetOneById(id);

        if (existingJob != null)
            await _jobRepository.Remove(id);
    }

    public async Task Check(Guid id, RepositoryType repositoryType)
    {
        _jobRepository = _repositoryFactory.GetJobRepository(repositoryType);

        var existingJob = await _jobRepository.GetOneById(id);
        if (existingJob == null)
            throw new ArgumentNullException("The job was not found and its status cannot be changed.");

        if (existingJob.IsCompleted)
            await _jobRepository.Uncheck(id);
        else
            await _jobRepository.Check(id);
    }
}