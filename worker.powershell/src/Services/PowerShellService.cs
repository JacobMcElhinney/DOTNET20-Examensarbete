using System.Management.Automation;
using worker.powershell.src.Interfaces;

namespace worker.powershell.src.Services
{
    ///<Summary>
    ///
    ///</Summary>
    public class PowerShellService : IPowerShellService
    {
        public async Task<PSDataCollection<PSObject>> RunScript(string script)
        {
             using (PowerShell ps = PowerShell.Create())
            {
                try
                {
                    // specify the script code to run.
                    ps.AddScript(script);

                    // execute the script and await the result.
                    var pipelineObjects = await ps.InvokeAsync().ConfigureAwait(false);

                    return pipelineObjects;
                }
                catch (System.Exception error)
                {
                    return error.Message;
                }

            }
        }
    }
}

/* Developer note:  Once Group Policy Issue is resolved, attempt this solution:

                using (PowerShell ps = PowerShell.Create())
                {
                    // specify the script code to run.
                    ps.AddScript("scriptContents");
                    
                    // specify the parameters to pass into the script.
                    ps.AddParameters(scriptParameters);

                    // execute the script and await the result.
                    var pipelineObjects = await ps.InvokeAsync().ConfigureAwait(false);

                    // print the resulting pipeline objects to the console.
                    foreach (var item in pipelineObjects)
                    {
                        Console.WriteLine(item.BaseObject.ToString());
                    }
*/