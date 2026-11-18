using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SusanOsuolaleCV.Data;
using SusanOsuolaleCV.Models;
using SusanOsuolaleCV.Models.DTOs;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using SusanOsuolaleCV.Api.Services;

namespace SusanOsuolaleCV.Api.Controllers
{
    [ApiController]
    [Route("api/gallery")]
    public class GalleryApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IGalleryBlobStorage _blobStorage;
        private readonly ILogger<GalleryApiController> _logger;

        public GalleryApiController(AppDbContext context, IGalleryBlobStorage blobStorage, ILogger<GalleryApiController> logger)
        {
            _context = context;
            _blobStorage = blobStorage;
            _logger = logger;
        }

        /// <summary>
        /// Get all gallery images.
        /// </summary>
        /// <returns>List of gallery image DTOs.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading gallery images");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Retrieve a file from Azure Blob Storage.
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns>The file stream</returns>
        [HttpGet("files/{**blobName}")]
        public async Task<IActionResult> GetFileFromBlob(string blobName)
        {
            try
            {
                // Normalize to forward slashes for blob paths
                var blobPath = blobName.Replace("\\", "/");
                var (stream, contentType) = await _blobStorage.DownloadAsync(blobPath);
                var mimeType = contentType ?? "application/octet-stream";
                // Return inline image view, not a forced download
                return File(stream, mimeType);
            }
            catch (FileNotFoundException)
            {
                // Blob genuinely doesn't exist
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Blob download error for blob '{BlobName}'", blobName);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
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

            // Upload to blob storage via service (no local disk usage)
            var blobName = await _blobStorage.UploadAsync(file, file.ContentType);

            var model = new GalleryImage
            {
                FileName = blobName,
                Title = title ?? string.Empty,
                Description = description ?? string.Empty
            };

            _context.GalleryImages.Add(model);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Roll back uploaded blob if DB save fails to avoid orphaned blob
                await _blobStorage.DeleteAsync(blobName);
                _logger.LogError(ex, "DB save failed after blob upload for blob '{BlobName}'", blobName);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }

            var resultDto = new GalleryImageDto { Id = model.Id, FileName = model.FileName, Title = model.Title, Description = model.Description };
            return CreatedAtAction(nameof(GetAll), new { }, resultDto);
        }

        
    }
}
