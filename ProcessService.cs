namespace worker.powershell
{
    public class ProcessService : IProcessService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
 
        public ProcessService(HttpClient httpClient, string remoteServiceBaseUrl)
        {
            _httpClient = httpClient;
            _remoteServiceBaseUrl = remoteServiceBaseUrl;
        }

        //Q&A: ska det vara step eller steps? "GetNextSteps.ts" renamed to GetProceedingSteps.
        public async Task<Step> GetProceedingSteps()
        {
            //! Write C# implementation of worker.javascript\client\GetNextSteps.ts
            return proceedingSteps;
        }

           public async Task<Step> SetNextSteps()
        {
            //! Write C# implementation of worker.javascript\client\GetNextSteps.ts
            return proceedingSteps;
        }
    }
}
