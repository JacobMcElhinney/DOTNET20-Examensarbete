using System.Management.Automation;

namespace worker.powershell.src.Interfaces
{
    public interface IPowerShellService
    {
        Task<PSDataCollection<PSObject>> RunScript(string script);
    }
    
}