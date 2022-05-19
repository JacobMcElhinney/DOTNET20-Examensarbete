namespace worker.powershell.src.Interfaces
{
    public interface ILog<T>
    {
        DateTime? TimeStamp { get; set; }
        string? ProcessId { get; set; }
        int? ProcessStep { get; set; }
        string? User { get; set; }
        string? Message { get; set; } //! Make non-nullanne
        string? Serverity { get; set; }//! Make non-nullanne
        T LogSeverity {get;set;}
    }
}   