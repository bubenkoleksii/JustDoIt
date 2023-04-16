using AutoMapper;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Response;
using JustDoIt.DAL.Interfaces;

namespace JustDoIt.BLL.Implementations.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;

    private readonly IMapper _mapper;

    public JobService(IJobRepository jobRepository, IMapper mapper)
    {
        _jobRepository = jobRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<JobModelResponse>> GetAll(bool sortByStatus = true, bool sortByDueDate = true)
    {
        var jobs = await _jobRepository.GetAll();

        var jobsResponse = _mapper.Map<IEnumerable<JobModelResponse>>(jobs);
        return jobsResponse.ToList();
    }
}