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
        public Priority Priority { get; set; }

        [JsonPropertyName("jobType")]
        public Status Status { get; set; }
        
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        
        [JsonPropertyName("completed")]
        public DateTime? Completed { get; set; }

    }


    public enum Priority
    {
        Low, Meduium, High
    }

   public enum Status
        {
            Pending,
            Started,
            Cancelled,
            Completed,
            
        }


}


