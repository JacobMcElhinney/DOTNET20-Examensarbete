using System.Text;
using System.Text.Json;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using static System.Net.Mime.MediaTypeNames;

namespace worker.powershell.src.Services
{
    ///<Summary>
    /// Calls external API, returns WorkerJobs and updates the status of WorkerJobs once processed by the Woker.
    ///</Summary>
    public class JobService : IJobService<WorkerJob>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _namedClient;

        public JobService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _namedClient = "JobApiClient";
        }

        public async Task<List<WorkerJob>> GetJobsAsync()
        {
            using (HttpClient jobApiClient = _httpClientFactory.CreateClient(_namedClient))
            {
                try
                {
                    string uri = jobApiClient.BaseAddress.ToString() + "api/Job";

                    var streamTask = jobApiClient.GetStreamAsync(requestUri: uri);

                    var jobs = await JsonSerializer.DeserializeAsync<List<WorkerJob>>(utf8Json: await streamTask);

                    return jobs;

                }
                catch (Exception e)
                {
                    System.Console.WriteLine($"{nameof(JobService)}: {e.Message}. Make sure API is running");
                    return null;
                }
            }
        }

        public async Task PutJobAsync(WorkerJob job) 
        {
            if (job.Status.ToString() == "Completed")
                job.Completed = DateTime.Now;
                

            HttpClient jobApiClient = _httpClientFactory.CreateClient(_namedClient);
            

                var jsonJob = new StringContent(
                    JsonSerializer.Serialize<WorkerJob>(job),
                    Encoding.UTF8,
                    Application.Json);

                
                var httpResponseMessage = await jobApiClient.PutAsync(requestUri: $"/api/Job/{job.Id}", jsonJob); //api/Job/{id}
                httpResponseMessage.EnsureSuccessStatusCode();
        }


        //! For testing/demonstration purposes only.
        public async Task ResetJobsInDb(WorkerJob job)//! Remove after testing
        {
            job.Completed = null;
            job.Status = WorkerJob.StatusType.Pending;

            HttpClient jobApiClient = _httpClientFactory.CreateClient(_namedClient);
            

                var jsonJob = new StringContent(
                    JsonSerializer.Serialize<WorkerJob>(job),
                    Encoding.UTF8,
                    Application.Json);

                
                var httpResponseMessage = await jobApiClient.PutAsync(requestUri: $"/api/Job/{job.Id}", jsonJob); //api/Job/{id}
                httpResponseMessage.EnsureSuccessStatusCode();
            
        }


    }
}