namespace SusanOsuolaleCV.Models.DTOs
{
    public class ProjectItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
    }
}