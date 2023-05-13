﻿using GraphQL.Types;
using JustDoIt.WebApp.Models.Request;

namespace JustDoIt.WebApp.GraphQl.Category.Types;

public class CategoryRequestType : ObjectGraphType<CategoryRequest>
{
    public CategoryRequestType()
    {
        Name = nameof(CategoryRequest);

        Field(i => i.Name, nullable: false)
            .Description("Name field for category object");
    }
}