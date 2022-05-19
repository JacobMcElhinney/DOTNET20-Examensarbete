namespace worker.powershell.src.Interfaces
{
    public interface ILogService<T>
    {
        Task postLog(ILog<T> log);
    }
}