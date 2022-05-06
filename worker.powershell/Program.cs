using worker.powershell;
using worker.powershell.src.Models;
using worker.powershell.src.Services;
using worker.powershell.src.Interfaces;

//Add configuration capabilitie
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        IConfiguration configurationRoot = context.Configuration; //Provide access to appsettings via merged configuration
        services.Configure<WorkerOptions>(configurationRoot.GetSection(key: nameof(WorkerOptions))); //Bind WorkerOptions to configuration section by key and add WorkerOptions to DI container
        services.AddHostedService<Worker>(); //Add worker service to the container.
        services.AddHttpClient<IProcessService<ProcessStep>, ProcessService>();// create a standard HttpClient for each service and register services as transient so they can be injected and consumed directly without any need for additional registrations.
        services.AddTransient<PowerShellClient>(); //! Transient or scoped/singleton or even static class?

    }).Build();


//.NET will start the worker service.
await host.RunAsync();
