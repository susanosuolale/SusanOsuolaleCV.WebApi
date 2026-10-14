using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Data;
using SusanOsuolaleCV.Models;
using SusanOsuolaleCV.Models.DTOs;

namespace SusanOsuolaleCV.Api.Controllers
{
    [ApiController]
    [Route("api/experience")]
    public class ExperienceApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ExperienceApiController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.WorkItems
                .AsNoTracking()
                .Select(w => new WorkItemDto
                {
                    Id = w.Id,
                    Title = w.Title,
                    Company = w.Company,
                    Period = w.Period,
                    Responsibilities = w.Responsibilities
                })
                .ToListAsync();

            return Ok(list);
        }
    }
}
