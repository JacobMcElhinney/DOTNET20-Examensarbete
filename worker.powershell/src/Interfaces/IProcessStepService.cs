namespace worker.powershell.src.Interfaces
{
    public interface IProcessStepService<T>
    {
        //! Refactor!
        Task<List<T>> GetPendingSteps(); //! Return type should be a collection of T as it returns several ProcessStep instances.
        Task<T> SetStepStatus();

    }
}