## Scrapped classes


### Coravel
```Csharp


//FROM Program.cs
using Coravel;

services.AddScheduler(); //Register Coravel's scheduler
services.AddTransient<ProcessOrder>(); //lifetime of service instance resolved registered as transient: new instance constructed on each request.

host.Services.UseScheduler(scheduler =>
{
    var jobSchedule = scheduler.Schedule<ProcessOrder>();
    jobSchedule.EverySeconds(2);
    //!jobSchedule.Cron("* * * * *"); use string interpolation and dependency injection. cronExpression = $"{minute} {hour} {dayOfMonth} {month} {dayOfWeek}"; - get from environment variable/yaml/config?
});

//FROM ProcessOrder Class

using Coravel.Invocable;
using Microsoft.Extensions.Options;
using worker.powershell;
using worker.powershell.src.Models;
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

```


```CSharp

//Two how to get current directory or path realtive to where this code is being executed from.
            var files = Directory.GetFiles(".");
            foreach (string file in files)
            {
                System.Console.WriteLine("worker directory: " + Path.GetRelativePath(".",file));
            }

            //! TEST: refactor this code, create method for navigating to script directory/env file etc etc..
            if (EnvironmentVariables == null)
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, $"Create env file here: {currentDirectory}{_options.Path}");
            }

```


```CSharp

//DIFFERENT APPROACH TO POWERSHELL CLIENT... Execution Policy GPO conflict?
/*
     [!]: File C:\Custom\Repositories\Sprinto\DOTNET20-Examensarbete\worker.powershell\src\Scripts\LogEntryScript.ps1 cannot be loaded because running scripts is disabled on this system. For more information, see about_Execution_Policies at https://go.microsoft.com/fwlink/?LinkID=135170. Job status: Cancelled
*/

//Worker.cs
 var output = _powerShellService.RunScript(job.Path);
                        
                        foreach (var item in output)
                        {
                            Terminal.LogMessage(Terminal.MessageType.Info, $"PowerShellService: {item.BaseObject.ToString()}");
                        }; 

//Needs refactoring this seems to be working but PowershellService is registered as transient and I want have i registered as a singleton and open/close runspace in the run script method instead.

//PowerShellService.cs
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using worker.powershell.src.Interfaces;

namespace worker.powershell.src.Services
{
    public class PowerShellService : IPowerShellService
    {
        private Runspace _runspace;
        private PowerShell _ps;

        public PowerShellService()
        {
            _runspace = RunspaceFactory.CreateRunspace();
            _runspace.Open();
            _ps = PowerShell.Create(_runspace);
        }
        public Collection<PSObject> RunScript(string scriptPath)
        {
            Collection<PSObject> output;
            using (var pipeline = _runspace.CreatePipeline())
            {
                System.Console.WriteLine(scriptPath + "-------------------------------------");
               Command runScript = new Command(scriptPath);
               pipeline.Command.Add()
               pipeline.Commands.Add(runScript);
               var pipelineObjects = pipeline.Invoke();
               output = pipelineObjects;
            }
            return output;
        }
    }
}

/*
OUTPUT
task: Job 9 Status: Started
.\src\Scripts\LogEntryScript.ps1-------------------------------------
 [!]: File C:\Custom\Repositories\Sprinto\DOTNET20-Examensarbete\worker.powershell\src\Scripts\LogEntryScript.ps1 cannot be loaded because running scripts is disabled on this system. For more information, see about_Execution_Policies at https://go.microsoft.com/fwlink/?LinkID=135170. Job status: Cancelled
*/

```

