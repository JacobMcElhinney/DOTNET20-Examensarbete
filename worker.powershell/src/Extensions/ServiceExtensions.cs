using System.Net.Http.Headers;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Services;

namespace worker.powershell.src.Extensions
{
    ///<summary>
    /// Provides Extension Methods for registring services and groups of related dependencies with the .NET Dependency Injection Container.
    ///</summary>
    public static class ServiceExtensions
    {

        public static void RegisterHttpClients(this IServiceCollection services, IConfiguration appSettingsConfiguration)
        {
            //Developer note: the only HTTP Client fully implemented
            services.AddHttpClient<IJobService<WorkerJob>, JobService>(name: "JobApiClient", client => 
            {
                try
                {    
                    client.BaseAddress = new Uri(appSettingsConfiguration["WorkerOptions:SqlTestApi"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("User-Agent", (
                        appSettingsConfiguration["WorkerOptions:Name"] + "/" + appSettingsConfiguration["WorkerOptions"] + "JobApiClient"));
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Failed to register client JobApiClient: " +  e.Message);
                }
            }).SetHandlerLifetime(TimeSpan.FromMinutes(2));

            //Developer note: the services listed below are yet to be fully implemented

        //     services.AddHttpClient<IProcessStepService<ProcessStep>, ProcessStepService>(name: "FlowApiClient", client =>
        //     {
        //         try
        //         {
        //             client.BaseAddress = new Uri(appSettingsConfiguration["WorkerOptions:FlowApiBaseUrl"]); //Since Process Step relies on Flow api.
        //             client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //IANA registered Discrete Type (application: generic binary data) representing a single json text stream
        //             client.DefaultRequestHeaders.Add("User-Agent", (appSettingsConfiguration["WorkerOptions:Name"] + "/" + appSettingsConfiguration["WorkerOptions"] + "FlowApiClient"));
        //         }
        //         catch (Exception e)
        //         {
        //             System.Console.WriteLine("RegisterHttpClients: " + e);
        //         }



        //     }).SetHandlerLifetime(TimeSpan.FromMinutes(2));// create a standard HttpClient for each service and register services as transient so they can be injected and consumed directly without any need for additional registrations.

        //     services.AddHttpClient<IProcessStepService<ProcessStep>, ProcessStepService>(name: "GitHubClient", configureClient: client => //Update to new classes...
        //     {

        //         //Depending on the services I register I will set the base address to match the corresponding external service/API
        //         client.BaseAddress = new Uri(appSettingsConfiguration["WorkerOptions:GitUrl"]); //Since Process Step relies on Flow api.
        //         client.DefaultRequestHeaders.Add(name: "Accept", value: "");

        //         //Use HTTP Standard header: Example: User-Agent: CERN-LineMode/2.15 libwww/2.17b3
        //         client.DefaultRequestHeaders.Add(
        //             name: "User-Agent",
        //             value: (appSettingsConfiguration["WorkerOptions:Name"] + "/" + appSettingsConfiguration["WorkerOptions"] + "GitHubClient"));

        //     }).SetHandlerLifetime(TimeSpan.FromMinutes(2));// create a standard HttpClient for each service and register services as transient so they can be injected and consumed directly without any need for additional registrations.

        //     services.AddHttpClient<ILogService<Log>, LogService>(name: "LogApiClient", configureClient: client => //Update to new classes...
        //   {

        //       //Depending on the services I register I will set the base address to match the corresponding external service/API
        //       client.BaseAddress = new Uri(appSettingsConfiguration["WorkerOptions:LogApiBaseUrl"]); //Since Process Step relies on Flow api.
        //       client.DefaultRequestHeaders.Add(name: "Accept", value: "");

        //       //Use HTTP Standard header: Example: User-Agent: CERN-LineMode/2.15 libwww/2.17b3
        //       client.DefaultRequestHeaders.Add(
        //           name: "User-Agent",
        //           value: (appSettingsConfiguration["WorkerOptions:Name"] + "/" + appSettingsConfiguration["WorkerOptions"] + "LogApiClient"));

        //   }).SetHandlerLifetime(TimeSpan.FromMinutes(2));// create a standard HttpClient for each service and register services as transient so they can be injected and consumed directly without any need for additional registrations.

        }
    }
}