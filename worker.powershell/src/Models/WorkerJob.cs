using System.Text.Json.Serialization;

namespace worker.powershell.src.Models
{
    /// <summary>
    /// Represents a Job object returned from the external Test API.
    /// </summary>
    public class WorkerJob
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        //Contains a relative path to a PowerShell script that can be executed once the job is being performed.
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("priority")]
        public PriorityLevel Priority { get; set; }

        [JsonPropertyName("status")]
        public StatusType Status { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("completed")]
        public DateTime? Completed { get; set; }

        public enum PriorityLevel
        {
            Low, Meduium, High
        }

        public enum StatusType
        {
            Pending,
            Started,
            Cancelled,
            Completed,

        }

    }

}


