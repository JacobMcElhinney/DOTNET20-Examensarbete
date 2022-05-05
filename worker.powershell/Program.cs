using worker.powershell;
using Coravel;

//Load .env file
string root = Directory.GetCurrentDirectory();
string dotenv = Path.Combine(root, ".env");
DotEnv.Load(dotenv);

var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

//Dependency injection container
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>(); //Add worker service to the container. 
        services.AddScheduler(); //Register Coravel's scheduler
        services.AddTransient<ProcessOrder>(); //lifetime of service instance resolved registered as transient: new instance constructed on each request.
        services.AddTransient<PowerShellClient>(); //! Transient or scoped/singleton or even static class?
    })
    .Build();

host.Services.UseScheduler(scheduler =>
{
    var jobSchedule = scheduler.Schedule<ProcessOrder>();
    jobSchedule.EverySeconds(2);
    //!jobSchedule.Cron("* * * * *"); use string interpolation and dependency injection. cronExpression = $"{minute} {hour} {dayOfMonth} {month} {dayOfWeek}"; - get from environment variable/yaml/config?
});


//.NET will start the worker service.
await host.RunAsync();
