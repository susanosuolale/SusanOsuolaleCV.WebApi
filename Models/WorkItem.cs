using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SusanOsuolaleCV.Models
{
    public class WorkItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;

        // Persist responsibilities as JSON in the database
        public string ResponsibilitiesJson { get; set; } = "[]";

        [NotMapped]
        public List<string> Responsibilities
        {
            get => JsonSerializer.Deserialize<List<string>>(ResponsibilitiesJson) ?? new List<string>();
            set => ResponsibilitiesJson = JsonSerializer.Serialize(value ?? new List<string>());
        }
    }
}