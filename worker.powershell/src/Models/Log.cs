
using System.ComponentModel;
using worker.powershell.src.Interfaces;

namespace worker.powershell.src.Models
{
    public class Log : ILog<Log.LogSeverity>
    {
        public DateTime? TimeStamp { get; set; }
        public string? ProcessId { get; set; }
        public int? ProcessStep { get; set; }
        public string? User { get; set; }
        public string Message { get; set; }
        public string Serverity { get; set; }
        LogSeverity ILog<LogSeverity>.LogSeverity { get; set; }
        

         public enum LogSeverity
        {
            [Description("Warning")]
            Warning,

            [Description("Error")]
            Error,

            [Description("Information")]
            Information,

        }
 

    }

}