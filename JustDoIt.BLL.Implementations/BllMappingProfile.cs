using AutoMapper;
using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.DAL.Entities.Request;
using JustDoIt.DAL.Entities.Response;

namespace JustDoIt.BLL.Implementations;

public class BllMappingProfile : Profile
{
    public BllMappingProfile()
    {
        CreateMap<JobEntityResponse, JobModelResponse>();
        CreateMap<JobModelRequest, JobEntityRequest>();
    }
}