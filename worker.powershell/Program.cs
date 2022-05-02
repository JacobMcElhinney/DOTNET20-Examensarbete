using worker.powershell;

//Dependency injection container
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //Add worker service to the container. 
        services.AddHostedService<Worker>();
    })
    .Build();

//.NET will start the worker service.
await host.RunAsync();
