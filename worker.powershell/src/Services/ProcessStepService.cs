using Newtonsoft.Json;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;

namespace worker.powershell.src.Services
{
    public class ProcessStepService : IProcessStepService<ProcessStep>
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public ProcessStepService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //! worker.javascript\src\client\getNextSteps.ts
        public Task<ProcessStep> GetPendingSteps() //!Borde return type vara process? returnerar proces.steps?
        {
            //! Do I really need a using statement? I think .Net will dispose of the instance since it manages all dependencies..
            using (HttpClient flowApiClient = _httpClientFactory.CreateClient("FlowApiClient"))
            {
                var fake = new ProcessStep(){Agent = "test", Language="test", Path="test"};
                try
                {
                    var endpoint = flowApiClient.BaseAddress;
                    var result = flowApiClient.GetAsync(endpoint).Result; //!Return only the result
                    var json = result.Content.ReadAsStringAsync().Result;
                }
                catch(Exception e)
                {
                    System.Console.WriteLine("Failed to retrieve pending process steps: " + e.Message);
                }
                finally
                {
                    
                    System.Console.WriteLine("so far so good...");
              

                }
                      return null;
            }



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
        }

        public Task<ProcessStep> SetStepStatus()
        {
            throw new NotImplementedException();
        }
    }
    //! Write C# implementation of worker.javascript\client\SetNextSteps.ts

}
