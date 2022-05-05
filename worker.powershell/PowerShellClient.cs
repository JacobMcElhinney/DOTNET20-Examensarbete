using System.Collections.ObjectModel;
using System.Management.Automation;

namespace worker.powershell
{
    //! Should I register this class as singleton in DI Container?
    public class PowerShellClient
    {
        public string RunScript (string path)
        {
            var s= path;
            System.Console.WriteLine("Script ran");
            return s;
        }

    }
}




