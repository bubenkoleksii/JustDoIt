using JustDoIt.BLL.Implementations.Services;
using JustDoIt.BLL.Interfaces;
using JustDoIt.DAL.Implementations;
using JustDoIt.DAL.Implementations.Repositories;
using JustDoIt.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DI
builder.Services.AddSingleton<DbFactory>();
builder.Services.AddScoped<IJobRepository, JobMsSqlServerRepository>();
builder.Services.AddScoped<IJobRepository, JobXmlRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryMsSqlServerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryXmlRepository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

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

app.MapControllerRoute(
    "default",
    "{controller=Index}/{action=Index}/{id?}");

app.Run();