using System.Management.Automation;
using worker.powershell.src.Interfaces;

namespace worker.powershell.src.Services
{
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


                    // print the resulting pipeline objects to the console.
                    // foreach (var item in pipelineObjects)
                    // {
                    //     System.Console.WriteLine("PowerShellClient: " + item.BaseObject.ToString());
                    // }

                    
                    ps.Dispose(); //! unnecessary call? will using statement ensure everything is disposed of correctly?

                    return pipelineObjects;
                }
                catch (System.Exception error)
                {
                    ps.Dispose();
                    return error.Message;
                }

            }
        }
    }
}