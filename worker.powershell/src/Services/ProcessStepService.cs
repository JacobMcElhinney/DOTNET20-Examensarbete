using System.Text.Json;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;
using System.Collections.Generic;

namespace worker.powershell.src.Services
{
    public class ProcessStepService : IProcessStepService<ProcessStep>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ProcessStepService(IHttpClientFactory httpClientFactory, IConfiguration appSettingsConfiguration )
        {
            _httpClientFactory = httpClientFactory;
            _configuration = appSettingsConfiguration;
        }

        //! worker.javascript\src\client\getNextSteps.ts
        public async Task<List<ProcessStep>> GetPendingSteps() //!Borde return type vara process? returnerar proces.steps?
        {
            //! Do I really need a using statement? I think .Net will dispose of the instance since it manages all dependencies..
            using (HttpClient flowApiClient = _httpClientFactory.CreateClient("FlowApiClient"))
            {
                try
                {
                    string url = flowApiClient.BaseAddress.ToString();
                    string agent = _configuration["WorkerOptions:AgentName"];
                    
                    //Call GetStreamAsync and return and return response body of type Task<Stream>.
                    var streamTask = flowApiClient.GetStreamAsync(requestUri: $"{url}agent/{agent}/next");

                    //Return a collection of ProcessSteps from streamTask where Json key name matches [JsonPropertyName("<propertyName>")] attribute in ProcessStep class.
                    var processSteps = await JsonSerializer.DeserializeAsync<List<ProcessStep>>(await streamTask);
                    
                    return processSteps;

                    
                }
                catch (Exception e)
                {
                    DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Error, e.Message);
                    DebuggingAssistant.LogMessage(DebuggingAssistant.MessageType.Info, "ProcessStepsService: Fake data returned for demonstration purposes");
                    return MockData.GenerateProcessStepsCollection();
                }
    
            }


        }

        public Task<ProcessStep> SetStepStatus()
        {
            throw new NotImplementedException();
        }
    }
    //! Write C# implementation of worker.javascript\client\SetNextSteps.ts

}
