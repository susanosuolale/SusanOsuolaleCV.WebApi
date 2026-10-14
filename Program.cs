using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SusanOsuolaleCV.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Reuse the same connection string from main project
var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Server=(localdb)\\mssqllocaldb;Database=AppDb;Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

// Serve static files (to expose images from this project's wwwroot)
builder.Services.AddDirectoryBrowser();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SusanOsuolaleCV API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Configure static file serving from this project's wwwroot path
var localRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (Directory.Exists(localRoot))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(localRoot),
        RequestPath = ""
    });
}

app.UseRouting();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SusanOsuolaleCV API v1");
    c.RoutePrefix = "swagger"; // Serve Swagger UI at /swagger
});

app.MapControllers();

app.Run();
