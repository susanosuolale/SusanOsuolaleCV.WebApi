using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Models;

namespace SusanOsuolaleCV.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Education> Educations { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<ProjectItem> Projects { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        // Seed data for CV
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Education>().HasData(
                new Education { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Degree = "B.Eng. Electronic & Electrical Engineering", Institution = "Obafemi Awolowo University", Years = "2022", Details = "" }
            );

            modelBuilder.Entity<WorkItem>().HasData(
                new WorkItem { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Title = "Backend Software Developer", Company = "Self-Directed / Personal Projects", Period = "Feb 2026 - Present", ResponsibilitiesJson = "[\"C# and ASP.NET Core for building REST APIs\",\"SQL Server, PostgreSQL, EF Core\",\"Redis, Docker, Clean Architecture, Microservices\",\"Background processing and messaging\"]" },
                new WorkItem { Id = Guid.Parse("22222222-2222-2222-2222-222222222223"), Title = "Data Analyst / Data Scientist", Company = "Various", Period = "2023 - 2024", ResponsibilitiesJson = "[\"Python, Pandas, NumPy, Scikit-learn\",\"Exploratory data analysis and visualization\",\"Statistical analysis and machine learning\"]" },
                new WorkItem { Id = Guid.Parse("22222222-2222-2222-2222-222222222224"), Title = "Lead Generator", Company = "Freelance", Period = "2024 - 2025", ResponsibilitiesJson = "[\"Researched and identified potential leads\",\"Maintained prospect lists for outreach\"]" },
                new WorkItem { Id = Guid.Parse("22222222-2222-2222-2222-222222222225"), Title = "Building Engineer", Company = "MEE Consults", Period = "2023", ResponsibilitiesJson = "[\"Electrical distribution and lighting systems\",\"Emergency power and low-voltage systems\",\"AutoCAD electrical layouts and coordination\"]" }
            );

            modelBuilder.Entity<ProjectItem>().HasData(
                new ProjectItem { Id = Guid.Parse("33333333-3333-3333-3333-333333333331"), Title = "SchoolMaster", Summary = "Multi-tenant school management backend.", TagsJson = "[\"C#\",\"ASP.NET Core\",\"EF Core\",\"PostgreSQL\",\"Redis\",\"Docker\"]", Status = "Completed" },
                new ProjectItem { Id = Guid.Parse("33333333-3333-3333-3333-333333333332"), Title = "FreelanceFlow", Summary = "Invoice/payment microservice platform.", TagsJson = "[\" .NET\",\"CQRS\",\"RabbitMQ\",\"PostgreSQL\",\"Elasticsearch\"]", Status = "In Progress" },
                new ProjectItem { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Title = "TaskPulse", Summary = "ASP.NET Core Web API project.", TagsJson = "[\"C#\",\"ASP.NET Core\",\"Redis\",\"Docker\"]", Status = "Completed" },
                new ProjectItem { Id = Guid.Parse("33333333-3333-3333-3333-333333333334"), Title = "AI Subscription Platform", Summary = "AI-powered subscription platform.", TagsJson = "[\"FastAPI\",\"OpenAI\",\"PostgreSQL\",\"Docker\",\"Paystack\"]", Status = "Completed" }
            );

            modelBuilder.Entity<GalleryImage>().HasData(
                new GalleryImage { Id = Guid.Parse("44444444-4444-4444-4444-444444444441"), FileName = "photo1.jpg", Title = "Profile Shot", Description = "Professional headshot." },
                new GalleryImage { Id = Guid.Parse("44444444-4444-4444-4444-444444444442"), FileName = "project1.png", Title = "SchoolMaster Screenshot", Description = "Screenshot of SchoolMaster project." }
            );

            modelBuilder.Entity<Profile>().HasData(
                new Profile { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "OJONE SUSAN OSUOLALE", Title = "Backend Software Engineer", Bio = "Backend Software Engineer with experience designing secure, scalable RESTful APIs using C#, ASP.NET Core, EF Core, SQL Server, and PostgreSQL." }
            );
        }
    }
}