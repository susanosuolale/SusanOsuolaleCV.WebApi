namespace SusanOsuolaleCV.Models.DTOs
{
    public class EducationDto
    {
        public Guid Id { get; set; }
        public string Degree { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Years { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}