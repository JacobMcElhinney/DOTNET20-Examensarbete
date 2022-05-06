namespace worker.powershell 
{
    /// <summary>
    /// Option class (implementation of Options pattern in .NET), used to read related configuration values in appsettings.Environment.json 
    /// </summary>

    //Type representation of configuration section in appsettings
    public class WorkerOptions 
    {
        //Representing a configuration value in e.g appsettings.Development.json or appsettings.json
        public string? Name { get; set; }
        public string? Logging { get; set; }

    }

    /* DEVELOPER NOTES:
        Please refer to the documentation
        https://docs.microsoft.com/en-us/dotnet/core/extensions/options
        
    */
}