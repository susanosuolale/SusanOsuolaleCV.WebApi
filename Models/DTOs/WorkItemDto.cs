namespace SusanOsuolaleCV.Models.DTOs
{
    public class WorkItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public List<string> Responsibilities { get; set; } = new List<string>();
    }
}