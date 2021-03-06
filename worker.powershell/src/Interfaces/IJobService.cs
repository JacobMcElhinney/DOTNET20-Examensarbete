namespace worker.powershell.src.Interfaces
{
    public interface IJobService<T>
    {
        Task<List<T>> GetJobsAsync();
        Task PutJobAsync(T job);

        //Developer note: remove method after development phase concludes.
        Task ResetJobsInDb(T job);

    }
}