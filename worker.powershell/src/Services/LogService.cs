using System.Net.Http.Json;
using Newtonsoft.Json;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;
using worker.powershell.src.Utilities;

namespace worker.powershell.src.Services
{
    public class LogService : ILogService<Log>
    {
        private IHttpClientFactory _httpClientFactory;

        public LogService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        public async Task postLog(Log log)
        {
            try
            {
                await Task.Run(async () =>
                {
                    HttpClient logApiClient = _httpClientFactory.CreateClient("LogApiClient");
                    string flowApiUrl = logApiClient.BaseAddress.ToString();
                    var response = await logApiClient.PostAsJsonAsync(flowApiUrl, MockData.GenerateLog()); //! Expect exception to be trown when server is not running: no connection could be made...
                    var result = response.IsSuccessStatusCode ? "successful" : "failed";
                    System.Console.WriteLine("Log operation was " + result);
                });

                var task = Task.CompletedTask;
                System.Console.WriteLine("Operation complete: " + task.IsCompleted);

            }
            catch (Exception e)
            {
                System.Console.WriteLine("EXCEPTION: " + e.Message);
            }
        }
    }
}
