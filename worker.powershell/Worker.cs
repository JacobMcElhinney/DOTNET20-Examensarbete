namespace worker.powershell;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private int logEntryCount; 


    //Called once when resolved from Dependency Injection container in Program.cs
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Worker has run {logEntryCount} times", DateTimeOffset.Now);
            logEntryCount++;
            await Task.Delay(2000, stoppingToken); //! defaut value: 1000ms
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
