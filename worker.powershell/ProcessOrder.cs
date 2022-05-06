using Coravel.Invocable;
using Microsoft.Extensions.Options;
using worker.powershell;
/// <summary>
/// For testing purposes only. Remove from Repository
/// </summary>
public class ProcessOrder : IInvocable
{
    private ILogger<ProcessOrder> _logger;

    //!NOTE TO SELF: No external dependencies. keep classes reusable. Load worker options into Worker class and use that class to manipulate control flow.
    private WorkerOptions _options;

    //Instance registered as transient in Program.cs and resolved from it's DI container.
    public ProcessOrder(ILogger<ProcessOrder> logger, IOptions<WorkerOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }
    
    //Called by the Coravel task scheduler when the job is due.
    public async Task Invoke()
    {
        //! Logging can be enabled/disabled using WorkerOptions section in appsettings
        if(_options.Logging != "disabled")
        {
            //! This code needs refactoring.
             Guid jobId = Guid.NewGuid();
            _logger.LogInformation($"Job id: {jobId} started...");   //! replace state "stated" with actual state from "project vocabulary".
            await Task.Delay(3000); //! Added to simulate task execution of long running task...
            _logger.LogInformation($"job with id: {jobId} completed successfully.");
        }
        else if (_options.Logging == "disabled")
        {
            System.Console.WriteLine("To enable logging modify appsettings.Environment.json > WorkerOptions:Logging");
        }

    }
}
