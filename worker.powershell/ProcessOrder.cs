using Coravel.Invocable;

public class ProcessOrder : IInvocable
{
    private ILogger<ProcessOrder> _logger;

    //Instance registered as transient in Program.cs and resolved from it's DI container.
    public ProcessOrder(ILogger<ProcessOrder> logger)
    {
        _logger = logger;
    }
    
    //Called by the Coravel task scheduler when the job is due.
    public async Task Invoke()
    {
        Guid jobId = Guid.NewGuid();
        _logger.LogInformation($"Job id: {jobId} started...");   //! replace state "stated" with actual state from "project vocabulary".
       await Task.Delay(3000); //! Added to simulate task execution of long running task...
       _logger.LogInformation($"job with id: {jobId} completed successfully.");

    }
}
