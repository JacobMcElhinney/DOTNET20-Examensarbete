using worker.powershell;
using Coravel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;




//Add configuration capabilitie
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>(); //Add worker service to the container. 
        services.AddScheduler(); //Register Coravel's scheduler
        services.AddTransient<ProcessOrder>(); //lifetime of service instance resolved registered as transient: new instance constructed on each request.
        services.AddTransient<PowerShellClient>(); //! Transient or scoped/singleton or even static class?
    }).ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();

        IHostEnvironment env = hostingContext.HostingEnvironment;

        configuration
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

        IConfigurationRoot configurationRoot = configuration.Build();

        WorkerOptions options = new();
        configurationRoot.GetSection(nameof(WorkerOptions))
                         .Bind(options);

        Console.WriteLine($"WorkerOptions.Name={options.Name}");
    }).Build();




host.Services.UseScheduler(scheduler =>
{
    var jobSchedule = scheduler.Schedule<ProcessOrder>();
    jobSchedule.EverySeconds(2);
    //!jobSchedule.Cron("* * * * *"); use string interpolation and dependency injection. cronExpression = $"{minute} {hour} {dayOfMonth} {month} {dayOfWeek}"; - get from environment variable/yaml/config?
});


//.NET will start the worker service.
await host.RunAsync();
