using JustDoIt.BLL.Implementations.Services;
using JustDoIt.BLL.Interfaces;
using JustDoIt.DAL.Implementations;
using JustDoIt.DAL.Interfaces;
using JustDoIt.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DI
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var xmlConnectionFactory = new XmlConnectionFactory(builder.Configuration);
var msSqlServerConnectionFactory = new MsSqlServerConnectionFactory(builder.Configuration);

builder.Services.AddScoped<Func<StorageType, IJobRepository>>(serviceProvider =>
    storageType => RepositoryFactory.GetJobRepository(xmlConnectionFactory, msSqlServerConnectionFactory, storageType));

builder.Services.AddScoped<Func<StorageType, ICategoryRepository>>(serviceProvider =>
    storageType => RepositoryFactory.GetCategoryRepository(xmlConnectionFactory, msSqlServerConnectionFactory, storageType));


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