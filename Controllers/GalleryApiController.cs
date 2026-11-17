using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Data;
using SusanOsuolaleCV.Models;
using SusanOsuolaleCV.Models.DTOs;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SusanOsuolaleCV.Api.Controllers
{
    [ApiController]
    [Route("api/gallery")]
    public class GalleryApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly BlobContainerClient _blobContainerClient;

        public GalleryApiController(AppDbContext context, BlobContainerClient blobContainerClient)
        {
            _context = context;
            _blobContainerClient = blobContainerClient;
        }

        /// <summary>
        /// Get all gallery images.
        /// </summary>
        /// <returns>List of gallery image DTOs.</returns>
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

        /// <summary>
        /// Retrieve a file from Azure Blob Storage.
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns>The file stream</returns>
        [HttpGet("files/{fileName}")]
        public async Task<IActionResult> GetFileFromBlob(string fileName)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName.Replace("\\", "/"));

            if (await blobClient.ExistsAsync())
            {
                BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();
                var stream = blobDownloadInfo.Content;
                string mimeType = blobDownloadInfo.ContentType ?? "application/octet-stream";
                return File(stream, mimeType, fileName);
            }

            return NotFound();
        }

        /// <summary>
        /// Upload a new image to Azure Blob Storage.
        /// </summary>
        /// <param name="dto">The data transfer object containing the image file and metadata</param>
        /// <returns>The created gallery image DTO</returns>
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
            var savedName = Path.Combine("files", Guid.NewGuid().ToString() + ext);
            var blobClient = _blobContainerClient.GetBlobClient(savedName);
            
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
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
