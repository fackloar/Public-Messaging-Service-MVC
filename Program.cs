using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MessagingService.Data;
using MessagingService.Models;
using MessagingService.Scheduler;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;
using MessagingService.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MessagingServiceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MessagingServiceContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    q.ScheduleJob<SendMailJob>(trigger => trigger
    .StartNow()
    .WithSimpleSchedule(builder => builder.RepeatForever().WithIntervalInHours(24))
    );
});
builder.Services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<MessagingServiceContext>();
        SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger>();
        logger.LogError(ex, "An error while seeding the db");
    }
}




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Email}/{action=Index}/{id?}");
app.Run();

