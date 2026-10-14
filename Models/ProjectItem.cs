using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SusanOsuolaleCV.Models
{
    public class ProjectItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        // Persist tags as JSON
        public string TagsJson { get; set; } = "[]";

        [NotMapped]
        public List<string> Tags
        {
            get => JsonSerializer.Deserialize<List<string>>(TagsJson) ?? new List<string>();
            set => TagsJson = JsonSerializer.Serialize(value ?? new List<string>());
        }
    }
}