using Core.Interfaces;
using Infrastructure;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration().MinimumLevel.Override("Microsoft",LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console().CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    builder.Host.UseSerilog((context,services,configuration)=>configuration
    .ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services)
    .Enrich.FromLogContext());
    builder.Logging.AddSerilog();
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<DataContext>();
    builder.Services.AddControllersWithViews();
    builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    var app = builder.Build();
    app.UseSerilogRequestLogging(configure =>
    {
        configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.000} ms";
    });
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host Terminated");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
return 0;

