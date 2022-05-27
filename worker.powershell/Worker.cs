using Microsoft.Extensions.Options;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;
using System.Management.Automation;

namespace worker.powershell;

/// <summary>
/// Service runs continuously until cancellation token is requested or the the service fails to connect to external API. Worker options such as cycleInterval, external API source etc can be configured in appsettings.<Environment>.json
/// </summary>

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly WorkerOptions _options;
    private readonly IPowerShellService _powerShellService;
    private readonly IJobService<WorkerJob> _jobService;
    private int IterationCount { get; set; }

    //Constructor called once, when resolved from Dependency Injection container in Program.cs
    public Worker(ILogger<Worker> logger, IOptions<WorkerOptions> options, IPowerShellService powerShellService, IJobService<WorkerJob> jobService)
    {
        _logger = logger;
        //Appsettings.Development.json section: "WorkerOptions": 
        _options = options.Value;
        _powerShellService = powerShellService;
        _jobService = jobService;

        //Enable/disable development logging in terminal in appsettings.Development.json
        Terminal.Logging = _options.Logging;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running...");

        while (!stoppingToken.IsCancellationRequested)
        {
            Terminal.LogMessage(Terminal.MessageType.Info, "Looking for work...");
            var jobs = await _jobService.GetJobsAsync();

            //Validation during development phase: break loop if exterenal test API does not return any jobs
            if (jobs is null)
            {
                Terminal.LogMessage(
                    Terminal.MessageType.Error, "Worker JobService failed to connect. Press [ctrl + c] to exit application...");
                break;
            }

            //For development purposes only: In production the API will not retun jobs where status is completed.
            var pendingJobs = jobs.Where<WorkerJob>(j => j.Status != WorkerJob.StatusType.Completed);

            foreach (var job in pendingJobs)
            {

                if (job.Status is not WorkerJob.StatusType.Completed && job.Completed == null) //Complete
                {
                    //For development purposes only: increace InterationCount for each job undertaken in pendingJobs
                    IterationCount++;

                    try
                    {
                        job.Status = WorkerJob.StatusType.Started;
                        Terminal.LogMessage(Terminal.MessageType.Info, $"Job {IterationCount} Status: {job.Status.ToString()}");

                        //Use PowerShellService to run pipeline objects one script parsed from job.Path is executed.
                        PSDataCollection<PSObject> output = await _powerShellService.RunScript(
                            ScriptParser.GetScriptFromPath(job.Path));

                        foreach (PSObject item in output)
                        {
                            Terminal.LogMessage(Terminal.MessageType.Info, $"PowerShellService: {item.BaseObject.ToString()}");
                        }

                        //For testing purposes only, Validate existense of file log.txt
                        var logfile = File.ReadAllText(job.Path);
                        if (logfile != null)
                        {
                            job.Status = WorkerJob.StatusType.Completed;
                            Terminal.LogMessage(Terminal.MessageType.Info, $"Job {IterationCount} Status: {job.Status.ToString()}");
                        }
                        else
                        {
                            Terminal.LogMessage(Terminal.MessageType.Error, $"Job {IterationCount}: {job.Status.ToString()} but failed to complete");
                            job.Status = WorkerJob.StatusType.Cancelled;
                        }

                        //Update job status
                        await _jobService.PutJobAsync(job);

                    }
                    catch (Exception e)
                    {
                        job.Status = WorkerJob.StatusType.Cancelled;
                        Terminal.LogMessage(Terminal.MessageType.Error, $"{e.Message} Job status reverted to: {job.Status.ToString()}");

                    }
                }
            }

            //If TestData in appsettings.Development.json is set to true, try to reset each job's status in the database, allowing them to be performed once more. 
            try
            {
                if (_options.TestData == true)
                {
                    foreach (var job in jobs)
                    {
                        await _jobService.ResetJobsInDb(job);
                    }
                }
            }
            catch (System.Exception e)
            {
                Terminal.LogMessage(Terminal.MessageType.Error, e.Message);
            }

            //Delay loop iteration (cycleInterval) to save compute power and costs once deployed as an Azure WebJob. 
            await Task.Delay(_options.CycleInterval, stoppingToken);
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