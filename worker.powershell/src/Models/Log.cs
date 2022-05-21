namespace worker.powershell.src.Models
{
    public class Log
    {
        public DateTime? TimeStamp { get; set; }
        public string? ProcessId { get; set; }
        public int? ProcessStep { get; set; }
        public string? User { get; set; }
        public string? Message { get; set; }
        public string? Serverity { get; set; }

         public enum LogSeverity
        {
            Warning,
            Error,
            Information,
        }
 

    }

}