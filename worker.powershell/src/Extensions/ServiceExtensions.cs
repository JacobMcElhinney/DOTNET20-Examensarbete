using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Services;

namespace worker.powershell.src.Extensions
{
    ///<Summary>
    /// Extension methods for registring services and groups of related dependencies with the .NET Dependency Injection Container..
    ///</Summary>
    public static class ServiceExtensions //! consider using generics to make these HTTP extensions work with other DI containers e.g. AutoFac etc for future use.
    {
        public static void RegisterHttpClients(this IServiceCollection services, IConfiguration appSettingsConfiguration)
        {
            //! DONT FORGET TO CHANGE THE INTERFACES AND CLASSES REGISTERED TO THE CORRECT ONES!
            services.AddHttpClient<IProcessStepService<ProcessStep>, ProcessStepService>(name: "FlowApiClient", client =>
            {
               try{
                   client.BaseAddress = new Uri(appSettingsConfiguration["WorkerOptions:FlowApiBaseUrl"]); //Since Process Step relies on Flow api.
               }
               catch (Exception e)
               {
                   System.Console.WriteLine( "Det sket sig");
               }
                
                client.DefaultRequestHeaders.Add(
                    name: "User-Agent",
                    value: (appSettingsConfiguration["WorkerOptions:Name"] + "/" + appSettingsConfiguration["WorkerOptions"] + "FlowApiClient"));
            }).SetHandlerLifetime(TimeSpan.FromMinutes(2));// create a standard HttpClient for each service and register services as transient so they can be injected and consumed directly without any need for additional registrations.

            services.AddHttpClient<IProcessStepService<ProcessStep>, ProcessStepService>(name: "GitHubClient", configureClient: client => //Update to new classes...
            {

                    //Depending on the services I register I will set the base address to match the corresponding external service/API
                client.BaseAddress = new Uri(appSettingsConfiguration["WorkerOptions:GitUrl"]); //Since Process Step relies on Flow api.
                client.DefaultRequestHeaders.Add(name: "Accept", value: "");

                    //Use HTTP Standard header: Example: User-Agent: CERN-LineMode/2.15 libwww/2.17b3
                client.DefaultRequestHeaders.Add(
                    name: "User-Agent",
                    value: (appSettingsConfiguration["WorkerOptions:Name"] + "/" + appSettingsConfiguration["WorkerOptions"] + "GitHubClient"));
            }).SetHandlerLifetime(TimeSpan.FromMinutes(2));// create a standard HttpClient for each service and register services as transient so they can be injected and consumed directly without any need for additional registrations.
        }
    }
}

/*
        DEVELOPER NOTES: to be removed
            Whether we use extension methods or just the basic methods in Program.cs, we will reach a point where 
            all of our services are now in our container. But how do we inject them into dependent classes?

            try: https://exceptionnotfound.net/dependency-injection-in-dotnet-6-adding-and-injecting-dependencies/

*/