﻿using AutoMapper;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.DAL.Entities.Request;
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

    public async Task<ICollection<JobModelResponse>> GetAll(bool sortByDueDate = true)
    {
        var jobs = await _jobRepository.GetAll();

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
        {
            await _jobRepository.Remove(id);
        }
    }

    public async Task Check(Guid id)
    {
        var existingJob = await _jobRepository.GetOneById(id);
        if (existingJob == null)
        {
            throw new ArgumentNullException();
        }

        if (existingJob.IsCompleted)
        {
            await _jobRepository.Uncheck(id);
        }
        else
        {
            await _jobRepository.Check(id);
        }
    }
}