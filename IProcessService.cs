namespace worker.powershell 
{
    public interface IProcessService
    {
        //! Refactor!
        async Task<Step> GetProceedingSteps();
        async Task<Step> SetProceedingSteps();

    }
}