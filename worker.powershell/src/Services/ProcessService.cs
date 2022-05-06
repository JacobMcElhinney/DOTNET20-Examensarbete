using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;

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

        public Task<ProcessStep> GetPendingSteps()
        {
            throw new NotImplementedException();
        }

        public Task<ProcessStep> SetStepStatus()
        {
            throw new NotImplementedException();
        }
    }
        //! Write C# implementation of worker.javascript\client\SetNextSteps.ts
    
}
