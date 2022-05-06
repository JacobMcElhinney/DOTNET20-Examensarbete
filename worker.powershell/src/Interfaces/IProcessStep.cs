namespace worker.powershell.src.Interfaces
{
    public interface IProcessStep<T>
    {
        string Language { get; set; }
        string Agent { get; set; }
        string Path { get; set; }
        IList<Object> Parameters { get; set; }
        T ProcessStepStatus { get; set; }

  
    }
}