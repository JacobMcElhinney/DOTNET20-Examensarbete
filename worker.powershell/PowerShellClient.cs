using System.Management.Automation;

namespace worker.powershell
{
    static class PowerShellClient
    {
        public static void Run()
        {
            using (var powershell = PowerShell.Create())
            {
                powershell.AddScript();
            }
        }

    }
}




