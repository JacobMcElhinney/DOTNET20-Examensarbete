using worker.powershell.src.Interfaces;

namespace worker.powershell.src.Models
{
    public class Process : IProcess<Process.Status>
    {
        public string FlowName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<object> Parameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<object> Steps { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        Status IProcess<Status>.Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public enum Status
        {
            Pending,
            Started,
            Completed,
            Error

        }
    }
}