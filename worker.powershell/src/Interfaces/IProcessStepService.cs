namespace worker.powershell.src.Interfaces
{
    public interface IProcessStepService<T>
    {
        //! Refactor!
        Task<T> GetPendingSteps(); //! Return type should be a collection of T as it returns several ProcessStep instances.
        Task<T> SetStepStatus();

    }
}