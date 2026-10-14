using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SusanOsuolaleCV.Models.DTOs
{
    public class GalleryUploadDto
    {
        [Required(ErrorMessage = "File is required.")]
        public IFormFile? File { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MinLength(1, ErrorMessage = "Title cannot be empty.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MinLength(1, ErrorMessage = "Description cannot be empty.")]
        public string? Description { get; set; }
    }
}
