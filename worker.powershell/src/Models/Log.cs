
// using System.ComponentModel;

// namespace worker.powershell.src.Models
// {
//     public class Log
//     {
//         public DateTime? TimeStamp { get; set; }
//         public string? ProcessId { get; set; }
//         public int? ProcessStep { get; set; }
//         public string? User { get; set; }
//         public string Message { get; set; }
//         public string Serverity { get; set; }

//         //Since Log.LogSeverity.Warning = 0, use var severity = Log.LogSeverity.Warning.DisplayName(); to get string value.
//         public enum LogSeverity 
//         {
//             [Description("Warning")]
//             Warning,

//             [Description("Error")]
//             Error,

//             [Description("Information")]
//             Information,

//         }

//     }

// }