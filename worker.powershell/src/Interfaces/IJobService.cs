namespace worker.powershell.src.Interfaces
{
    public interface IJobService<T>
    {
        Task<List<T>> GetJobsAsync();
        Task PutJobAsync(T job);

        //Developer note: For testing purposes only.
        Task ResetJobsInDb(T job);

    }
}