using worker.powershell.src.Interfaces;

namespace worker.powershell.src.Models
{

    public class ProcessStep : IProcessStep<ProcessStep.ProcessStepStatus>
    {
        public string Language { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Agent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Path { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<object> Parameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        ProcessStepStatus IProcessStep<ProcessStepStatus>.ProcessStepStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public enum ProcessStepStatus
        {
            Pending,
            Started,
            Completed,
            Error

        }
       
    }
}