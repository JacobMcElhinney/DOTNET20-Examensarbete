using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace worker.powershell.src.Models
{
    public class WorkerJob //Ambigous reference if class named Job
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

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


