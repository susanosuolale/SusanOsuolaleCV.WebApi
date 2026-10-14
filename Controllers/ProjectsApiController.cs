using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Data;
using SusanOsuolaleCV.Models;
using SusanOsuolaleCV.Models.DTOs;

namespace SusanOsuolaleCV.Api.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProjectsApiController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Projects
                .AsNoTracking()
                .Select(p => new ProjectItemDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Summary = p.Summary,
                    Status = p.Status,
                    Tags = p.Tags
                })
                .ToListAsync();
            return Ok(list);
        }
    }
}
