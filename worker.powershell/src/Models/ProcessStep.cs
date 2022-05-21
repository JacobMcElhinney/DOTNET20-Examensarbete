namespace worker.powershell.src.Models
{

    public class ProcessStep
    {
        public string? Language { get; set; }
        public string? Agent { get; set; }
        public string? Path { get; set; }
        public IList<object>? Parameters { get; set; }
        public enum ProcessStepStatus
        {
            Pending,
            Started,
            Completed,
            Error

        }
       
    }
}