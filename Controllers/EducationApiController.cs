using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Data;
using SusanOsuolaleCV.Models;
using SusanOsuolaleCV.Models.DTOs;

namespace SusanOsuolaleCV.Api.Controllers
{
    [ApiController]
    [Route("api/education")]
    public class EducationApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EducationApiController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Educations
                .AsNoTracking()
                .Select(e => new EducationDto
                {
                    Id = e.Id,
                    Degree = e.Degree,
                    Institution = e.Institution,
                    Years = e.Years,
                    Details = e.Details
                })
                .ToListAsync();

            return Ok(list);
        }
    }
}
