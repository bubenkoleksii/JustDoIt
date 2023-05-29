using AutoMapper;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Request;
using JustDoIt.Shared;
using JustDoIt.WebApp.GraphQl.Job.Types;
using JustDoIt.WebApp.Models.Request;

namespace JustDoIt.WebApp.GraphQl.Job;

public class JobMutation : ObjectGraphType
{
    public JobMutation(IHttpContextAccessor contextAccessor, IMapper mapper)
    {
        Field<BooleanGraphType>("add")
            .Argument<NonNullGraphType<JobRequestType>>("job")
            .Resolve()
            .WithScope()
            .WithService<IJobService>()
            .ResolveAsync(async (context, service) =>
            {
                var job = context.GetArgument<JobRequest>("job");
                var storageType =
                    StorageHelper.GetStorageTypeByString(contextAccessor.HttpContext!.Request.Headers["StorageType"]!);

                var jobRequest = mapper.Map<JobModelRequest>(job);

                await service.Add(jobRequest, storageType);
                return true;
            });

        Field<BooleanGraphType>("remove")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .Resolve()
            .WithScope()
            .WithService<IJobService>()
            .ResolveAsync(async (context, service) =>
            {
                var id = context.GetArgument<Guid>("id");
                var storageType =
                    StorageHelper.GetStorageTypeByString(contextAccessor.HttpContext!.Request.Headers["StorageType"]!);

                await service.Remove(id, storageType);
                return true;
            });

        Field<BooleanGraphType>("check")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .Resolve()
            .WithScope()
            .WithService<IJobService>()
            .ResolveAsync(async (context, service) =>
            {
                var id = context.GetArgument<Guid>("id");
                var storageType =
                    StorageHelper.GetStorageTypeByString(contextAccessor.HttpContext!.Request.Headers["StorageType"]!);

                await service.Check(id, storageType);
                return true;
            });
    }
}