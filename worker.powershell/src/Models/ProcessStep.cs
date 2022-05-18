using worker.powershell.src.Interfaces;

namespace worker.powershell.src.Models
{

    public class ProcessStep : IProcessStep<ProcessStep.ProcessStepStatus>
    {
        public string Language { get; set; }
        public string Agent { get; set; }
        public string Path { get; set; }
        public IList<object> Parameters { get; set; }
        ProcessStepStatus IProcessStep<ProcessStepStatus>.ProcessStepStatus { get; set; }

        public enum ProcessStepStatus
        {
            Pending,
            Started,
            Completed,
            Error

        }
       
    }
}