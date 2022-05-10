using Newtonsoft.Json;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;

namespace worker.powershell.src.Services
{
    public class ProcessService : IProcessService<ProcessStep>
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
 
        public ProcessService(HttpClient httpClient, string remoteServiceBaseUrl)
        {
            _httpClient = httpClient;
            _remoteServiceBaseUrl = remoteServiceBaseUrl;
        }

        public Task<ProcessStep> GetPendingSteps() //!Borde return type vara process? returnerar proces.steps?
        {
            // var uri = _remoteServiceBaseUrl + "other dynamic data, see javascript.worker";
            // try 
            // {
            //     //! Will the api return a json object or json stringified?
            //     var responseString = await _httpClient.GetStringAsync("test");
            //     var processSteps = JsonConvert.DeserializeObject<ProcessStep>(responseString);
            //     return processSteps;

            // }
            // catch(Exception e)
            // {
            //     System.Console.WriteLine( e.Message);

            // }
            throw new NotImplementedException();
        }

        public Task<ProcessStep> SetStepStatus()
        {
            throw new NotImplementedException();
        }
    }
        //! Write C# implementation of worker.javascript\client\SetNextSteps.ts
    
}
