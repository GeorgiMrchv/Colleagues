using Colleagues.Interfaces;
using Colleagues.Utilities;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog before building the app
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICollaborationCalculator, CollaborationCalculator>();
builder.Services.AddScoped<IFileValidator, FileValidator>();
builder.Services.AddScoped<ICSVParser, CsvParser>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
