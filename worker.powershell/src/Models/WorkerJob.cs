using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace worker.powershell.src.Models
{
    public class WorkerJob //Ambigous reference if class named Job
    {
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public Priority Priority { get; set; }
        public JobType JobType { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Completed { get; set; }

    }


    public enum Priority
    {
        Low, Meduium, High
    }

    public enum JobType
    {
        Script
    }


}


