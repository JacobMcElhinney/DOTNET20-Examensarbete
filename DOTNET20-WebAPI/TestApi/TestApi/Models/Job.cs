using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestApi.Models
{
    public class Job
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } //Assigned by Entity Framework
        
        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [Required]
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("path")]
        public string Path { get; set; }
        
        [JsonPropertyName("priority")]
        public Priority Priority { get; set; }
        
        [JsonPropertyName("status")]
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
