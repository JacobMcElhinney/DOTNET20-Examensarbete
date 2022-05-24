using worker.powershell;
using worker.powershell.src.Models;
using worker.powershell.src.Services;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Extensions;

//Configure HostBuilderContext (program configuration context) and IServiceCollection (Dependency Injection Container) instances.
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        //Provide access to root node (appsettings.<Environment>.json) and merge application configuration with IHost configuration.
        IConfiguration configurationRoot = context.Configuration;

        //Bind WorkerOptions to configuration section by key and add WorkerOptions to DI container
        services.Configure<WorkerOptions>(configurationRoot.GetSection(key: nameof(WorkerOptions))); 

        services.RegisterHttpClients(configurationRoot);

        services.AddHostedService<Worker>();

        services.AddTransient<IJobService<WorkerJob>, JobService>();
         
        services.AddTransient<IProcessStepService<ProcessStep>, ProcessStepService>();

        services.AddTransient<ILogService<Log>, LogService>();

        services.AddTransient<IPowerShellService, PowerShellService>();


    }).Build();

//.NET will start the worker service.
await host.RunAsync();


/*  DEVELOPER NOTES

    TODO:
    -   Consider which variables to store in appsettings and which to store in launchSettings.json 
        or .env file. see utility method DotEnv.... application secrets must be stored in a secure manner.
    -   Add all the necessary classes to DI container at some point.
    -   .Net Options Pattern provides a way for application to read value from appsettings.json OnChange without restarting.
        Is that something we want?
*/