namespace worker.powershell.src.Interfaces
{
    public interface IProcessStepService<T>
    {
        //! Refactor!
        Task<T> GetPendingSteps(); //! GetProceedingSteps, GetAllSteps, GetRemainingSteps, GetPendingSteps?
        Task<T> SetStepStatus();

    }
}