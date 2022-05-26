namespace worker.powershell.src.Interfaces
{
    public interface IJobService<T>
    {
        Task<List<T>> GetJobsAsync();
        Task PutJobAsync(T job);

        Task ResetJobsInDb(T job);

    }
}