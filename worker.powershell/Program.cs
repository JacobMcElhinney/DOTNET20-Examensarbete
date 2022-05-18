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

        //Register http client services 
        services.AddHttpClient<IProcessService<ProcessStep>, ProcessService>(name: "FlowApiClient", client =>
        {
            //Depending on the services I register I will set the base address to match the corresponding external service/API
            client.BaseAddress = new Uri(configurationRoot["WorkerOptions:FlowApiUrl"]); //Since Process Step relies on Flow api.
            client.DefaultRequestHeaders.Add(
                name: "User-Agent",
                value: (configurationRoot["WorkerOptions:Name"] + "/" + configurationRoot["WorkerOptions"] + "/FlowApiClient"));

        }).SetHandlerLifetime(TimeSpan.FromMinutes(2));// create a standard HttpClient for each service and register services as transient so they can be injected and consumed directly without any need for additional registrations.

        services.AddHttpClient<IProcessService<ProcessStep>, ProcessService>(name: "GitHubClient", configureClient: client => //Update to new classes...
        {

            //Depending on the services I register I will set the base address to match the corresponding external service/API
            client.BaseAddress = new Uri(configurationRoot["WorkerOptions:GitUrl"]); //Since Process Step relies on Flow api.
            client.DefaultRequestHeaders.Add(name: "Accept", value: "");

            //Use HTTP Standard header: Example: User-Agent: CERN-LineMode/2.15 libwww/2.17b3
            client.DefaultRequestHeaders.Add(
                name: "User-Agent",
                value: (configurationRoot["WorkerOptions:Name"] + "/" + configurationRoot["WorkerOptions"] + "/GitHubClient"));
        }).SetHandlerLifetime(TimeSpan.FromMinutes(2));// create a standard HttpClient for each service and register services as transient so they can be injected and consumed directly without any need for additional registrations.

        services.AddTransient<IPowerShellService, PowerShellService>();

    }).Build();



//.NET will start the worker service.
await host.RunAsync();


/*  DEVELOPER NOTES

    TODO:
    -   Consider which variables to store in appsettings and which to store in launchSettings.json 
        or .env file. see utility method DotEnv.... application secrets must be stored in a secure manner.
    -   Add all the necessary classes to DI container at some point.
    -   .Net Options Pattern provides a way for application to read value from appsettings.json OnChange.
        Is that something we want?
*/