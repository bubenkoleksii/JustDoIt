using AutoMapper;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using JustDoIt.BLL.Interfaces;
using JustDoIt.Shared;
using JustDoIt.WebApp.GraphQl.Job.Types;
using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.GraphQl.Job;

public class JobQuery : ObjectGraphType
{
    public JobQuery(IHttpContextAccessor contextAccessor, IMapper mapper)
    {
        Field<ListGraphType<JobResponseType>>("getAll")
            .Resolve()
            .WithScope()
            .WithService<IJobService>()
            .ResolveAsync(async (_, service) =>
            {
                var storageType =
                    XmlStorageHelper.GetStorageTypeByString(contextAccessor.HttpContext!.Request.Headers["StorageType"]!);

                var jobs = await service.GetAll(storageType);

                var jobsResponse = mapper.Map<ICollection<JobResponse>>(jobs);
                return jobsResponse;
            });

        Field<ListGraphType<JobResponseType>>("getByCategory")
            .Argument<NonNullGraphType<IdGraphType>>("categoryId")
            .Resolve()
            .WithScope()
            .WithService<IJobService>()
            .ResolveAsync(async (context, service) =>
            {
                var categoryId = context.GetArgument<Guid>("categoryId");
                var storageType =
                    XmlStorageHelper.GetStorageTypeByString(contextAccessor.HttpContext!.Request.Headers["StorageType"]!);

                var jobs = await service.GetByCategory(categoryId, storageType);

                var jobsResponse = mapper.Map<ICollection<JobResponse>>(jobs);
                return jobsResponse;
            });
    }
}