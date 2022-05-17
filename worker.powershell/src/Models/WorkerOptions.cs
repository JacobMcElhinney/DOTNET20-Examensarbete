namespace worker.powershell.src.Models
{
    /// <summary>
    /// Option class (implementation of Options pattern in .NET), used to read related configuration values in appsettings.Environment.json 
    /// </summary>

    //Type representation of configuration section in appsettings
    public class WorkerOptions 
    {
        //Each propery has a binding to a configuration value with corresponding name in e.g appsettings.Development.json or appsettings.json
        public string? Name { get; set; }
        public string? Version { get; set; }
        public string? Description { get; set; }
        public string? Main { get; set; }
        public string? Path { get; set; }


        public string? Logging { get; set; }
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