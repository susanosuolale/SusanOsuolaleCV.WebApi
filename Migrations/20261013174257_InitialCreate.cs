using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SusanOsuolaleCV.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Years = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagsJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponsibilitiesJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "Id", "Degree", "Details", "Institution", "Years" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "B.Eng. Electronic & Electrical Engineering", "", "Obafemi Awolowo University", "2022" });

            migrationBuilder.InsertData(
                table: "GalleryImages",
                columns: new[] { "Id", "Description", "FileName", "Title" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444441"), "Professional headshot.", "photo1.jpg", "Profile Shot" },
                    { new Guid("44444444-4444-4444-4444-444444444442"), "Screenshot of SchoolMaster project.", "project1.png", "SchoolMaster Screenshot" }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Bio", "Name", "Title" },
                values: new object[] { new Guid("55555555-5555-5555-5555-555555555555"), "Backend Software Engineer with experience designing secure, scalable RESTful APIs using C#, ASP.NET Core, EF Core, SQL Server, and PostgreSQL.", "OJONE SUSAN OSUOLALE", "Backend Software Engineer" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Status", "Summary", "TagsJson", "Title" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333331"), "Completed", "Multi-tenant school management backend.", "[\"C#\",\"ASP.NET Core\",\"EF Core\",\"PostgreSQL\",\"Redis\",\"Docker\"]", "SchoolMaster" },
                    { new Guid("33333333-3333-3333-3333-333333333332"), "In Progress", "Invoice/payment microservice platform.", "[\" .NET\",\"CQRS\",\"RabbitMQ\",\"PostgreSQL\",\"Elasticsearch\"]", "FreelanceFlow" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Completed", "ASP.NET Core Web API project.", "[\"C#\",\"ASP.NET Core\",\"Redis\",\"Docker\"]", "TaskPulse" },
                    { new Guid("33333333-3333-3333-3333-333333333334"), "Completed", "AI-powered subscription platform.", "[\"FastAPI\",\"OpenAI\",\"PostgreSQL\",\"Docker\",\"Paystack\"]", "AI Subscription Platform" }
                });

            migrationBuilder.InsertData(
                table: "WorkItems",
                columns: new[] { "Id", "Company", "Period", "ResponsibilitiesJson", "Title" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Self-Directed / Personal Projects", "Feb 2026 - Present", "[\"C# and ASP.NET Core for building REST APIs\",\"SQL Server, PostgreSQL, EF Core\",\"Redis, Docker, Clean Architecture, Microservices\",\"Background processing and messaging\"]", "Backend Software Developer" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), "Various", "2023 - 2024", "[\"Python, Pandas, NumPy, Scikit-learn\",\"Exploratory data analysis and visualization\",\"Statistical analysis and machine learning\"]", "Data Analyst / Data Scientist" },
                    { new Guid("22222222-2222-2222-2222-222222222224"), "Freelance", "2024 - 2025", "[\"Researched and identified potential leads\",\"Maintained prospect lists for outreach\"]", "Lead Generator" },
                    { new Guid("22222222-2222-2222-2222-222222222225"), "MEE Consults", "2023", "[\"Electrical distribution and lighting systems\",\"Emergency power and low-voltage systems\",\"AutoCAD electrical layouts and coordination\"]", "Building Engineer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "GalleryImages");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "WorkItems");
        }
    }
}
