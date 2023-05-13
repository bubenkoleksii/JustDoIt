using GraphQL.Types;
using JustDoIt.WebApp.Models.Response;

namespace JustDoIt.WebApp.GraphQl.Job.Types;

public class JobResponseType : ObjectGraphType<JobResponse>
{
    public JobResponseType()
    {
        Name = nameof(JobResponse);

        Field(i => i.Id, type: typeof(IdGraphType), nullable: false)
            .Description("Id field for job object");
        Field(i => i.CategoryId, type: typeof(IdGraphType), nullable: false)
            .Description("Category id field for job object");
        Field(i => i.CategoryName, nullable: false)
            .Description("Category name field for job object");
        Field(i => i.Name, nullable: false)
            .Description("Name field for job object");
        Field(i => i.IsCompleted, nullable: false, type: typeof(BooleanGraphType))
            .Description("Status field for job object");
        Field(i => i.DueDate, nullable: false, typeof(DateTimeGraphType))
            .Description("Due date field for job object");
        Field(i => i.DateDifferenceInMinutes, nullable: false, typeof(IntGraphType))
            .Description("Difference in minutes to now field for job object");
    }
}