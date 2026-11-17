using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Data;
using SusanOsuolaleCV.Models.DTOs;

namespace SusanOsuolaleCV.Api.Controllers
{
    [ApiController]
    [Route("api/profile")]
    /// <summary>
    /// Handles operations related to the user profile.
    /// </summary>
    public class ProfileApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProfileApiController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profile = await _db.Profiles.AsNoTracking().FirstOrDefaultAsync();
            if (profile == null) return NotFound();
            var dto = new ProfileDto
            {
                Id = profile.Id,
                Name = profile.Name,
                Title = profile.Title,
                Bio = profile.Bio
            };
            return Ok(dto);
        }
    }
}
