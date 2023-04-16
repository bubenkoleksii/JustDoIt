using AutoMapper;
using JustDoIt.BLL.Models.Request;
using JustDoIt.BLL.Models.Response;
using JustDoIt.WebApp.Models.Request;
using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp;

public class WebAppMappingProfile : Profile
{
    public WebAppMappingProfile()
    {
        CreateMap<JobModelResponse, JobResponse>();
        CreateMap<JobRequest, JobModelRequest>();
    }
}