namespace worker.powershell.src.Interfaces
{
    public interface IJobService<T>
    {
        Task<List<T>> GetJobsAsync();
    }
}