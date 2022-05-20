# DOTNET20-Examensarbete
Smesh Workflow Engine

## Useful links
- [Use the new programming style](https://docs.microsoft.com/en-us/dotnet/core/tutorials/top-level-templates#use-the-new-program-style)
- [Debugging](https://docs.microsoft.com/en-us/dotnet/core/tutorials/debugging-with-visual-studio-code?pivots=dotnet-6-0)

## [Program & Worker Service](https://docs.microsoft.com/en-us/dotnet/core/extensions/workers)



## Notes on Debugging & Release:

[Set up for terminal input](https://docs.microsoft.com/en-us/dotnet/core/tutorials/debugging-with-visual-studio-code?pivots=dotnet-6-0):
Open .vscode/launch.json: 
```json
  "console": "integratedTerminal",
```

[Use Release build configuration](https://docs.microsoft.com/en-us/dotnet/core/tutorials/debugging-with-visual-studio-code?pivots=dotnet-6-0#use-release-build-configuration)
```pwsh
  dotnet run --configuration Release
```

## Potential Privacy or Security Issues

```pwsh
Windows PowerShell
Copyright (C) Microsoft Corporation. All rights reserved.

Install the latest PowerShell for new features and improvements! https://aka.ms/PSWindows  

PS C:\Custom\Repositories\Sprinto\DOTNET20-Examensarbete\worker.powershell> dotnet new console --framework net6.0

Welcome to .NET 6.0!
---------
The .NET tools collect usage data in order to help us improve your experience. It is collected by Microsoft and shared with the community. You can opt-out of telemetry by setting the DOTNET_CLI_TELEMETRY_OPTOUT environment variable to '1' or 'true' using your favorite shell.

Read more about .NET CLI Tools telemetry: https://aka.ms/dotnet-cli-telemetry
----------------
Installed an ASP.NET Core HTTPS development certificate.
To trust the certificate run 'dotnet dev-certs https --trust' (Windows and macOS only).
Learn about HTTPS: https://aka.ms/dotnet-https
----------------
Write your first app: https://aka.ms/dotnet-hello-world
Find out what's new: https://aka.ms/dotnet-whats-new
Explore documentation: https://aka.ms/dotnet-docs
Report issues and find source on GitHub: https://github.com/dotnet/core
Use 'dotnet --help' to see available commands or visit: https://aka.ms/dotnet-cli
--------------------------------------------------------------------------------------
The template "Console App" was created successfully.

Processing post-creation actions...
Running 'dotnet restore' on C:\Custom\Repositories\Sprinto\DOTNET20-Examensarbete\worker.powershell\worker.powershell.csproj...
  Determining projects to restore...
  Restored C:\Custom\Repositories\Sprinto\DOTNET20-Examensarbete\worker.powershell\worker.powershell.csproj (in 62 ms).
Restore succeeded.

PS C:\Custom\Repositories\Sprinto\DOTNET20-Examensarbete\worker.powershell>
```


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