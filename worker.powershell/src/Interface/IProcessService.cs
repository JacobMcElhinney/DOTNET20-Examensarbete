namespace worker.powershell.src.Interfaces
{
    public interface IProcessService<T>
    {
        //! Refactor!
        Task<T> GetPendingSteps(); //! GetProceedingSteps, GetAllSteps, GetRemainingSteps, GetPendingSteps?
        Task<T> SetStepStatus();

    }
}