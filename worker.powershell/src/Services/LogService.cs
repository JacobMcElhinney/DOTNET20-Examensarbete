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

        public async Task postLog(Log log)//! remove nullable attrinute.
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


/*
using (HttpClient flowApiClient = _httpClientFactory.CreateClient("FlowApiClient"))
            {
                var fake = new ProcessStep() { Agent = "test", Language = "test", Path = "test" };//! REMOVE!
                try
                {
                    var endpoint = flowApiClient.BaseAddress;
                    var result = flowApiClient.GetAsync(endpoint).Result; //!Return only the result
                    var json = result.Content.ReadAsStringAsync().Result; //var processSteps = JsonConvert.DeserializeObject<ProcessStep>(result);

                    //var responseString = await _httpClient.GetStringAsync("test");
                    //     var processSteps = JsonConvert.DeserializeObject<ProcessStep>(responseString);
                    //     return processSteps;
                }
*/



/* CONVERT TO C#
const fetch = require("node-fetch");
import { Log } from "../../../lib/types/Log";

const postLog = async (
  { processId,processStep, message, severity } : Log
) => {
  await fetch(`${process.env.LOG_API_URI}/log`, {
    method: "post",
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify({
      processId,
      processStep,
      message,
      severity,
    }),
  });
};

export default postLog;
*/