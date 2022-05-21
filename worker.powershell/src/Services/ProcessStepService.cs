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
        public async Task<ProcessStep> GetPendingSteps() //!Borde return type vara process? returnerar proces.steps?
        {
            //! Do I really need a using statement? I think .Net will dispose of the instance since it manages all dependencies..
            using (HttpClient flowApiClient = _httpClientFactory.CreateClient("FlowApiClient"))
            {
                try
                {
                    var endpoint = flowApiClient.BaseAddress;
                    var result = flowApiClient.GetAsync(endpoint).Result; //!Return only the result
                    var json = result.Content.ReadAsStringAsync().Result; //var processSteps = JsonConvert.DeserializeObject<ProcessStep>(result);

                    //var responseString = await _httpClient.GetStringAsync("test");
                    //     var processSteps = JsonConvert.DeserializeObject<ProcessStep>(responseString);
                    //     return processSteps;
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("EXCEPTION: " + e.Message);
                }
    

                //! Refactor once you have the flow api up and running
                Task<ProcessStep> myResult = Task.FromResult(MockData.GenerateProcessStep());
                return await myResult;
            }


        }

        public Task<ProcessStep> SetStepStatus()
        {
            throw new NotImplementedException();
        }
    }
    //! Write C# implementation of worker.javascript\client\SetNextSteps.ts

}
