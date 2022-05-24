using System.Text.Json;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;

namespace worker.powershell.src.Services
{
    public class JobService : IJobService<WorkerJob>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JobService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<WorkerJob>> GetJobsAsync()
        {
           using( HttpClient jobApiClient = _httpClientFactory.CreateClient("JobApiClient"))
           {
               try
               {
                   //! compare runtime value with: https://localhost:7065/api/Job
                   string uri = jobApiClient.BaseAddress.ToString() + "api/Job";
                   
                   var streamTask = jobApiClient.GetStreamAsync(requestUri: uri);

                   var jobs = await JsonSerializer.DeserializeAsync<List<WorkerJob>>(utf8Json: await streamTask);

                   return jobs;

               }
               catch (Exception e)
               {
                   System.Console.WriteLine(nameof(JobService) + e.Message);
                   throw;
               }
           }
        }


    }
}