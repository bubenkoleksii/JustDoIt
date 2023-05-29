using AutoMapper;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using JustDoIt.BLL.Interfaces;
using JustDoIt.BLL.Models.Request;
using JustDoIt.Shared;
using JustDoIt.WebApp.GraphQl.Category.Types;
using JustDoIt.WebApp.Models.Request;

namespace JustDoIt.WebApp.GraphQl.Category;

public class CategoryMutation : ObjectGraphType
{
    public CategoryMutation(IHttpContextAccessor contextAccessor, IMapper mapper)
    {
        Field<BooleanGraphType>("add")
            .Argument<NonNullGraphType<CategoryRequestType>>("category")
            .Resolve()
            .WithScope()
            .WithService<ICategoryService>()
            .ResolveAsync(async (context, service) =>
            {
                var category = context.GetArgument<CategoryRequest>("category");
                var storageType =
                    StorageHelper.GetStorageTypeByString(contextAccessor.HttpContext!.Request.Headers["StorageType"]!);

                var categoryRequest = mapper.Map<CategoryModelRequest>(category);

                await service.Add(categoryRequest, storageType);
                return true;
            });

        Field<BooleanGraphType>("remove")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .Resolve()
            .WithScope()
            .WithService<ICategoryService>()
            .ResolveAsync(async (context, service) =>
            {
                var id = context.GetArgument<Guid>("id");
                var storageType =
                    StorageHelper.GetStorageTypeByString(contextAccessor.HttpContext!.Request.Headers["StorageType"]!);

                await service.Remove(id, storageType);
                return true;
            });
    }
}