using AutoMapper;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.BLL.Implementations.Services;

public class JobService : IJobService
{
    private readonly IStorageFactory _storageFactory;

    private readonly ICategoryRepository _categoryRepository;

    private readonly IJobRepository _jobRepository;

    private readonly IMapper _mapper;

    public JobService(IStorageFactory storageFactory, IMapper mapper)
    {
        _storageFactory = storageFactory;
        _mapper = mapper;
    }

    public async Task<ICollection<JobModelResponse>> GetAll(bool sortByDueDate = true)
    {
        var jobs = await _jobRepository.GetAll();

        var jobsResponse = _mapper.Map<IEnumerable<JobModelResponse>>(jobs);
        return jobsResponse.ToList();
    }

    public async Task<ICollection<JobModelResponse>> GetByCategory(Guid categoryId, bool sortByDueDate = true)
    {
        var existingCategory = await _categoryRepository.GetOneById(categoryId);
        if (existingCategory == null)
            throw new ArgumentNullException("The jobs was not selected because category not found.");

        var jobs = await _jobRepository.GetByCategory(categoryId);

        var jobsResponse = _mapper.Map<IEnumerable<JobModelResponse>>(jobs);
        return jobsResponse.ToList();
    }

    public async Task Add(JobModelRequest job)
    {
        var jobRequest = _mapper.Map<JobEntityRequest>(job);
        await _jobRepository.Add(jobRequest);
    }

    public async Task Remove(Guid id)
    {
        var existingJob = await _jobRepository.GetOneById(id);

        if (existingJob != null)
            await _jobRepository.Remove(id);
    }

    public async Task Check(Guid id)
    {
        var existingJob = await _jobRepository.GetOneById(id);
        if (existingJob == null)
            throw new ArgumentNullException("The job was not found and its status cannot be changed.");

        if (existingJob.IsCompleted)
            await _jobRepository.Uncheck(id);
        else
            await _jobRepository.Check(id);
    }
}