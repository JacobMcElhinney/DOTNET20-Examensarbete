using Microsoft.Extensions.Options;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;
using System.Management.Automation;
namespace worker.powershell;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly WorkerOptions _options;
    private readonly IPowerShellService _powerShellService;
    private readonly IJobService<WorkerJob> _jobService;
    // private readonly IProcessStepService<ProcessStep> _processStepService;
    private int IterationCount { get; set; }


    //Called once, when resolved from Dependency Injection container in Program.cs
    public Worker(ILogger<Worker> logger, IOptions<WorkerOptions> options, IPowerShellService powerShellService, IJobService<WorkerJob> jobService) //IProcessStepService<ProcessStep> processStepService, ILogService<Log> logService
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
            if (jobs is null)
            {
                DebuggingAssistant.LogMessage(
                DebuggingAssistant.MessageType.Error,
                "Worker JobService failed. Press [ctrl + c] to exit application...");
                break;
            }

            var pendingJobs = jobs.Where<WorkerJob>(j => j.Completed == null);

            foreach (var job in pendingJobs)
            {

                if (job.Status is not (Status)3 && job.Completed == null) //Complete
                {
                    IterationCount++;

                    try
                    {
                        job.Status = (Status)1; //Started
                        DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, $"Job {IterationCount} Status: {job.Status.ToString()}");

                        PSDataCollection<PSObject> output = await _powerShellService.RunScript(
                            ScriptParser.GetScriptFromPath(job.Path));
                        foreach (PSObject item in output)
                        {
                            DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, $"PowerShellService: {item.BaseObject.ToString()}");
                        }

                        var logfile = File.ReadAllText(job.Path);
                        if (logfile != null)
                        {
                            job.Status = (Status)3; //Completed
                            DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, $"Job {IterationCount} Status: {job.Status.ToString()}");
                        }
                        else
                        {
                            job.Status = (Status)2; //Cancelled
                        }

                        _jobService.PutJobAsync(job);

                    }
                    catch (Exception e)
                    {
                        job.Status = (Status)2; //Cancelled
                        DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Error, $"{e.Message} Job status: {job.Status.ToString()}");
                    }
                }
            }


            DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, "Looking for work...");

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