using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SusanOsuolaleCV.Data;
using Azure.Identity;
using Azure.Storage.Blobs;
using System;
using System.IO;
using SusanOsuolaleCV.Api.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services

  // Configure Azure Blob Storage client
     //C# object to communicate with azure blob storage
     var blobServiceClient = new BlobServiceClient(new Uri("https://susanosuolalecvstorage.blob.core.windows.net/"), new DefaultAzureCredential());
     //C# object to communicate with your container(gallery-images) on azure blob storage
     var blobContainerClient = blobServiceClient.GetBlobContainerClient("gallery-images");

     // Register blob container client and blob storage service (DI)
     builder.Services.AddSingleton(blobContainerClient);
     builder.Services.AddSingleton<SusanOsuolaleCV.Api.Services.IGalleryBlobStorage>(sp =>
     {
         return new SusanOsuolaleCV.Api.Services.AzureGalleryBlobStorage(blobContainerClient);
     });
builder.Services.AddControllers();

// Reuse the same connection string from configuration.
// Do NOT silently fall back to LocalDB in production.
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(conn))
{
    if (builder.Environment.IsDevelopment())
    {
        // In development, allow a local fallback for convenience
        conn = "Server=(localdb)\\mssqllocaldb;Database=AppDb;Trusted_Connection=True;MultipleActiveResultSets=true";
    }
    else
    {
        // In production require the connection string to be set
        throw new InvalidOperationException(
            "DefaultConnection is not configured. Set the DefaultConnection in your App Service connection strings.");
    }
}

// Database configuration with automatic retries for transient SQL errors
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conn, sqlOptions =>
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)));

// Serve static files
builder.Services.AddDirectoryBrowser();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SusanOsuolaleCV API",
        Version = "v1"
    });
    // Include XML comments in Swagger
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
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
    c.RoutePrefix = "swagger";
});

app.MapControllers();

app.Run();
