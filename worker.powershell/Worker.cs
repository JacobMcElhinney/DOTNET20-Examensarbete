using Microsoft.Extensions.Options;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;
using System.ComponentModel; //For enum attributes
using worker.powershell.src.Services;
using System.Management.Automation; 
namespace worker.powershell;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly WorkerOptions _options;
    private readonly IPowerShellService _powerShellService;
    private readonly IJobService<WorkerJob> _jobService;
    private readonly IProcessStepService<ProcessStep> _processStepService;
    private int IterationCount {get;set;}


    //Called once, when resolved from Dependency Injection container in Program.cs
    public Worker(ILogger<Worker> logger, IOptions<WorkerOptions> options, IPowerShellService powerShellService, IJobService<WorkerJob> jobService ) //IProcessStepService<ProcessStep> processStepService, ILogService<Log> logService
    {
        _logger = logger;
        _options = options.Value; //Appsettings.Development.json section: "WorkerOptions": 
        _powerShellService = powerShellService;
        _jobService = jobService;
        // _processStepService = processStepService;
        DebuggingAssistant.Logging = _options.Logging;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            var jobs = await _jobService.GetJobsAsync();
            var terminalOutput = jobs[0].Title; //I'm outputting nothing...JsonPropertyNameAttribute YES!! THATS IT ADD TO REST OF PROPS!!!
            System.Console.WriteLine("---" + terminalOutput + "---");
            System.Console.WriteLine("jobs: " + jobs);

            // var steps = await _processStepService.GetPendingSteps();
            // foreach(var step in steps)
            // {
            //     System.Console.WriteLine(step.ToString());
            // }
            

            try
            {
                // var path = powerShellClient.RunScript(_options.path)
                PSDataCollection<PSObject> output = await _powerShellService.RunScript(
                    ScriptParser.GetScriptFromPath(
                        MockData.ScriptPath));


                foreach (PSObject item in output)
                {
                    DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, $"PowerShellService: {item.BaseObject.ToString()}");
                }

            }
            catch (Exception e)
            {
                DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Error, e.Message);
            }




            await Task.Delay(_options.CycleInterval, stoppingToken); //! defaut value: 1000ms
        }
    }

    //Called when the worker is started. Allows for asynchronous initialization calls.
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker starting...");
        return base.StartAsync(cancellationToken);
    }

    //Called when the worker is stopped. Allows for cleanup calls.
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker stopping...");
        return base.StopAsync(cancellationToken);
    }
}