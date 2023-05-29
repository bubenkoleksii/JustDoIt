using GraphQL;
using JustDoIt.BLL.Implementations.Services;
using JustDoIt.BLL.Interfaces;
using JustDoIt.DAL.Implementations;
using JustDoIt.DAL.Implementations.Repositories;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;
using JustDoIt.WebApp.GraphQl.Category;
using JustDoIt.WebApp.GraphQl.Job;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DI
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddSingleton<XmlConnectionFactory>();
builder.Services.AddSingleton<MsSqlServerConnectionFactory>();

builder.Services.AddScoped<CategoryMsSqlServerRepository>();
builder.Services.AddScoped<CategoryXmlRepository>();
builder.Services.AddScoped<JobMsSqlServerRepository>();
builder.Services.AddScoped<JobXmlRepository>();

builder.Services.AddScoped<Func<StorageType, IJobRepository>>(serviceProvider =>
    storageType => RepositoryFactory.GetJobRepository(serviceProvider, storageType));

builder.Services.AddScoped<Func<StorageType, ICategoryRepository>>(serviceProvider =>
    storageType => RepositoryFactory.GetCategoryRepository(serviceProvider, storageType));

// GraphQl
builder.Services.AddGraphQL(builder => builder
    .AddSystemTextJson()
    .AddSchema<CategorySchema>()
    .AddSchema<JobSchema>()
    .AddGraphTypes(typeof(JobSchema).Assembly)
    .AddGraphTypes(typeof(CategorySchema).Assembly));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseGraphQL<JobSchema>("/job");
app.UseGraphQL<CategorySchema>("/category");

app.UseGraphQLAltair();

app.MapControllerRoute(
    "default",
    "{controller=Index}/{action=Index}/{id?}");

StorageHelper.CreateXmlStorageIfNotExists(app.Configuration.GetConnectionString("XmlStoragePath"),
    app.Configuration.GetConnectionString("XmlJobStoragePath"),
    app.Configuration.GetConnectionString("XmlCategoryStoragePath"));

app.Run();