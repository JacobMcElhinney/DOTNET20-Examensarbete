using System.Text.Json.Serialization;

namespace worker.powershell.src.Models
{

    public class ProcessStep
    {
        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("agent")]
        public string? Agent { get; set; }
        
        [JsonPropertyName("path")]
        public string? Path { get; set; }
        
        [JsonPropertyName("parameters")]
        public List<object>? Parameters { get; set; }
        public enum ProcessStepStatus
        {
            Pending,
            Started,
            Completed,
            Error

        }

    }
}