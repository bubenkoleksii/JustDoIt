﻿using GraphQL.Types;
using JustDoIt.WebApp.Models.Request;

namespace JustDoIt.WebApp.GraphQl.Job.Types;

public class JobRequestType : InputObjectGraphType<JobRequest>
{
    public JobRequestType()
    {
        Name = nameof(JobRequest);

        Field(i => i.CategoryId, type: typeof(IdGraphType), nullable: false)
            .Description("Category id field for job object");
        Field(i => i.Name, nullable: false)
            .Description("Name field for job object");
        Field(i => i.IsCompleted, nullable: false, type: typeof(BooleanGraphType))
            .Description("Status field for job object");
        Field(i => i.DueDate, nullable: false, typeof(DateTimeGraphType))
            .Description("Due date field for job object");
    }
}