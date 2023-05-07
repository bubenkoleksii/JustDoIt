using JustDoIt.BLL.Implementations.Services;
using JustDoIt.BLL.Interfaces;
using JustDoIt.DAL.Implementations;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DI
builder.Services.AddSingleton<MsSqlServerConnectionFactory>();
builder.Services.AddSingleton<XmlConnectionFactory>();

builder.Services.AddSingleton<IRepositoryFactory, RepositoryFactory>();

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

XmlStorageHelper.CreateXmlStorageIfNotExists(app.Configuration.GetConnectionString("XmlStoragePath"), 
    app.Configuration.GetConnectionString("XmlJobStoragePath"), 
    app.Configuration.GetConnectionString("XmlCategoryStoragePath"));

app.Run();