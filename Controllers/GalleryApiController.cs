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
                    FileName = g.FileName,:wq


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
            string homePath = Path.Combine(Environment.GetEnvironmentVariable("HOME") ?? "", "gallery-images");
            string wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            var imagesPath = Path.Combine(homePath, fileName);
            if (!System.IO.File.Exists(imagesPath))
            {
                imagesPath = Path.Combine(wwwrootPath, fileName);
                if (!System.IO.File.Exists(imagesPath)) return NotFound();
            }

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            var mime = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
            var bytes = System.IO.File.ReadAllBytes(imagesPath);
            return File(bytes, mime);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] GalleryUploadDto dto)
        {
            var file = dto?.File;
            var title = dto?.Title;
            var description = dto?.Description;

            if (file == null || file.Length == 0) return BadRequest("File is required.");

            string homePath = Path.Combine(Environment.GetEnvironmentVariable("HOME") ?? "", "gallery-images");
            if (!Directory.Exists(homePath)) Directory.CreateDirectory(homePath);

            var ext = Path.GetExtension(file.FileName);
            var savedName = Guid.NewGuid().ToString() + ext;
            var savePath = Path.Combine(homePath, savedName);

            using (var stream = System.IO.File.Create(savePath))
            {
                await file.CopyToAsync(stream);
            }

            var model = new GalleryImage
            {
                FileName = savedName,
                Title = title ?? string.Empty,
                Description = description ?? string.Empty
            };

            _context.GalleryImages.Add(model);
            await _context.SaveChangesAsync();

            var resultDto = new GalleryImageDto { Id = model.Id, FileName = model.FileName, Title = model.Title, Description = model.Description };
            return CreatedAtAction(nameof(GetAll), new { }, resultDto);
        }

        
    }
}
