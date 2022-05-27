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

        //Call extension method to register HttpClients and configure clients using IConfiguration instance.
        services.RegisterHttpClients(configurationRoot);

        services.AddHostedService<Worker>();

        services.AddTransient<IJobService<WorkerJob>, JobService>();

        services.AddTransient<IPowerShellService, PowerShellService>();


    }).Build();

//.NET will start the worker service.
await host.RunAsync();