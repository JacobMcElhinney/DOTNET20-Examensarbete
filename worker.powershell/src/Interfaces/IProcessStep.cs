namespace worker.powershell.src.Interfaces
{
    public interface IProcessStep<T>
    {
        string? Language { get; set; } //! Make non-nullanne
        string? Agent { get; set; } //! Make non-nullanne
        string? Path { get; set; } //! Make non-nullanne
        IList<Object>? Parameters { get; set; } //! Make non-nullanne
        T ProcessStepStatus { get; set; }

  
    }
}