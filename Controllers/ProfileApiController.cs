using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Data;
using SusanOsuolaleCV.Models.DTOs;

namespace SusanOsuolaleCV.Api.Controllers
{
    /// <summary>
    /// Handles operations related to the user profile.
    /// </summary>
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

        /// <summary>
        /// Retrieve the user profile.
        /// </summary>
        /// <returns>The user profile DTO.</returns>
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
