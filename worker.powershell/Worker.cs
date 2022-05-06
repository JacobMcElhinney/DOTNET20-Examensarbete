using System.Management.Automation;
using Microsoft.Extensions.Options;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;

namespace worker.powershell;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly WorkerOptions _options;

    //MockData
    private int logEntryCount { get; set; }


    //Called once when resolved from Dependency Injection container in Program.cs
    public Worker(ILogger<Worker> logger, IOptions<WorkerOptions> options)
    {
        _logger = logger;
        _options = options.Value; //Appsettings.Development.json section: "WorkerOptions": 
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker starting...");

        while (!stoppingToken.IsCancellationRequested)
        {
            PowerShellClient powerShellClient = new();
            var output = await powerShellClient.RunScript(
                ScriptParser.GetScriptFromPath(
                    MockData.ScriptPath));

            if (_options.Logging != "disabled")
            {
                foreach (PSObject item in output)
                { 
                    System.Console.WriteLine("PowerShellClient: " + item.BaseObject.ToString()); 
                }
            }


            await Task.Delay(3000, stoppingToken); //! defaut value: 1000ms
        }
    }

    //Called when the worker is started. Allows for asynchronous initialization calls.
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        if (_options.Logging != "disabled") _logger.LogInformation("Worker starting...");
        return base.StartAsync(cancellationToken);
    }

    //Called when the worker is stopped. Allows for cleanup calls.
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker stopping...");
        return base.StopAsync(cancellationToken);
    }
}
