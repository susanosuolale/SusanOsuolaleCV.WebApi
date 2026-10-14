using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Data;
using SusanOsuolaleCV.Models;
using SusanOsuolaleCV.Models.DTOs;

namespace SusanOsuolaleCV.Api.Controllers
{
    [ApiController]
    [Route("api/gallery")]
    public class GalleryApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GalleryApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.GalleryImages
                .AsNoTracking()
                .Select(g => new GalleryImageDto
                {
                    Id = g.Id,
                    FileName = g.FileName,
                    Title = g.Title,
                    Description = g.Description
                })
                .ToListAsync();
            return Ok(list);
        }


        // Serve the actual image file
        [HttpGet("files/{fileName}")]
        public IActionResult GetFile(string fileName)
        {
            var originalRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var imagesPath = Path.Combine(originalRoot, "images", fileName);
            if (!System.IO.File.Exists(imagesPath)) return NotFound();
            //image / jpeg or mime tells the browser the response contains a JPEG image.
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            var mime = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
            //reads all the bytes of the file and stores in memory
            var bytes = System.IO.File.ReadAllBytes(imagesPath);
            //sends the bytes back to whoever requested it
            return File(bytes, mime);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GalleryUploadDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var file = dto.File!;
            var title = dto.Title!;
            var description = dto.Description!;

            // Simple server-side checks
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext)) return BadRequest($"File type '{ext}' is not allowed.");
            if (file.Length == 0) return BadRequest("File is empty.");
            const long maxBytes = 5 * 1024 * 1024; // 5 MB
            if (file.Length > maxBytes) return BadRequest($"File exceeds maximum allowed size of {maxBytes} bytes.");

            var originalRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var imagesFolder = Path.Combine(originalRoot, "images");
            if (!Directory.Exists(imagesFolder)) Directory.CreateDirectory(imagesFolder);

            var savedName = Guid.NewGuid().ToString() + ext;
            var savePath = Path.Combine(imagesFolder, savedName);

            using (var stream = System.IO.File.Create(savePath))
            {
                await file.CopyToAsync(stream);
            }

            var model = new GalleryImage
            {
                FileName = savedName,
                Title = title,
                Description = description
            };

            _context.GalleryImages.Add(model);
            await _context.SaveChangesAsync();

            var resultDto = new GalleryImageDto { Id = model.Id, FileName = model.FileName, Title = model.Title, Description = model.Description };
            return CreatedAtAction(nameof(GetAll), new { }, resultDto);
        }

        
    }
}
