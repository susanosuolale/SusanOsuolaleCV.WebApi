using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Data;
using SusanOsuolaleCV.Models;
using SusanOsuolaleCV.Models.DTOs;

namespace SusanOsuolaleCV.Api.Controllers
{
    [ApiController]
    [Route("api/education")]
    /// <summary>
    /// Handles operations related to educational qualifications in the CV.
    /// </summary>
    public class EducationApiController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EducationApiController"/> class.
        /// </summary>
        /// <param name="db">The database context to be used.</param>
        private readonly AppDbContext _db;

        public EducationApiController(AppDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Retrieves all educational qualifications.
        /// </summary>
        /// <returns>A list of educational qualifications.</returns>

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
