using JustDoIt.BLL.Implementations.Services;
using JustDoIt.BLL.Interfaces;
using JustDoIt.DAL.Implementations;
using JustDoIt.DAL.Implementations.Repositories;
using JustDoIt.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// DI
builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();

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