using System.Text.Json.Serialization;

namespace worker.powershell.src.Models
{
    public class Log
    {
        [JsonPropertyName("timestamp")] //all properties need this attribute
        public DateTime? Timestamp { get; set; }
        
        [JsonPropertyName("processId")]
        public string? ProcessId { get; set; }
        
        [JsonPropertyName("processStep")]
        public int? ProcessStep { get; set; }
        
        [JsonPropertyName("user")]
        public string? User { get; set; }
        
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        
        [JsonPropertyName("serverity")]
        public string? Serverity { get; set; }

         public enum LogSeverity
        {
            Warning,
            Error,
            Information,
        }
 

    }

}