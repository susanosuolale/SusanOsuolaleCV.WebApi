using System.ComponentModel.DataAnnotations;

namespace SusanOsuolaleCV.Models
{
    public class Education
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Degree { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Years { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}