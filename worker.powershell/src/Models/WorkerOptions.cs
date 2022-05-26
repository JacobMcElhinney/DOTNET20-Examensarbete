namespace worker.powershell.src.Models
{
    /// <summary>
    /// A representation of the configuration section in appsettings. 
    /// WorkerOptions is used to read related configuration values in appsettings.Environment.json. 
    /// Each class member property has a binding to a configuration value with a corresponding name. 
    /// Options Class: implementation of Options pattern in .NET (Microsoft.Extensions.Options.IOptions).
    /// </summary>

    public class WorkerOptions 
    {
        public string? Name { get; set; }
        public string? Version { get; set; }
        public string? Description { get; set; }
        public string? Main { get; set; }
        public string? Path { get; set; }

        public bool? Logging { get; set; }
        public bool? TestData { get; set; }

        public string? SqlTestApi {get;set;}
        public string? LogApiBaseUrl { get; set; }
        public string? FlowApiBaseUrl { get; set; }
        public string? GitUrl { get; set; }
        public string? AgentName { get; set; }
        public int CycleInterval { get; set; }

        



    }

    /* DEVELOPER NOTES:
        Please refer to the documentation
        https://docs.microsoft.com/en-us/dotnet/core/extensions/options
        
    */
}