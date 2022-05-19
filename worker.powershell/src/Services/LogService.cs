using System.Net.Http.Json;
using Newtonsoft.Json;
using worker.powershell.src.Interfaces;
using worker.powershell.src.Models;

namespace worker.powershell.src.Services
{
    public class LogService : ILogService<Log>
    {
        private IHttpClientFactory _httpClientFactory;

        public LogService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        public async Task postLog(ILog<Log>? log)//! remove nullable attrinute.
        {
            try
            {
                await Task.Run(async () =>
                {
                    HttpClient logApiClient = _httpClientFactory.CreateClient("LogApiClient");
                    string flowApiUrl = logApiClient.BaseAddress.ToString();
                    var fakeLog = new Log();//! REmove fake log
                    var response = await logApiClient.PostAsJsonAsync(flowApiUrl, fakeLog); //! Expect exception to be trown when server is not running: no connection could be made...
                    var result = response.IsSuccessStatusCode ? "successful" : "failed";
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Log operation was " + result);
                });

                var task = Task.CompletedTask;
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("DEBUG: Operation complete: " + task.IsCompleted);

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
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