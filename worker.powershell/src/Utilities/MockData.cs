using System.Management.Automation;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using worker.powershell.src.Models;

namespace worker.powershell.src.Utilities
{
    /// <summary>
    /// Provides mock data for testing during development phase.
    /// </summary>
    public static class MockData
    {
        // private static string scriptPath = @"TestScript.ps1";
        private static string scriptPath = @".\src\Scripts\LogScript.ps1";

        public static string ScriptPath
        {
            get { return scriptPath; }
            set { scriptPath = value; }
        }

        //Used to request GitHub repositories
        private static HttpClient Client = new HttpClient();
        //Used to display repository names

        public static List<ProcessStep> GenerateProcessStepsCollection()
        {

            ProcessStep fakeStep1 = new() { Agent = "Cloud", Language = "English", Parameters = null, Path = "a" };
            ProcessStep fakeStep2 = new() { Agent = "Local", Language = "Swedish", Parameters = null, Path = "b" };
            var processSteps = new List<ProcessStep>();
            processSteps.Add(fakeStep1);
            processSteps.Add(fakeStep2);
            return processSteps;
        }

        public static Log GenerateLog()
        {
            Log fakeLog = new()
            {
                ProcessId = "test process",
                ProcessStep = 1,
                Serverity = Log.LogSeverity.Information.ToString(),
                Timestamp = DateTime.Now,
                User = "test",
                Message = "test"
            };
            return fakeLog;
        }

        public static ProcessStep GenerateProcessStep()
        {
            var fakeProcessStep = new ProcessStep() { Agent = "test", Language = "test", Path = "test" };
            return fakeProcessStep;
        }

        public static async Task GetReposRequest()
        {
            //Set up HTTP Headers for all requests:
            Client.DefaultRequestHeaders.Accept.Clear();
            //An accept header to accept JSON responses
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            //A User-Agent header. (checked by the GitHub server code, necessary to retrieve information from GitHub)
            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var uri = "https://api.github.com/orgs/dotnet/repos";
            
            //Make web request and retrieve the response: starts a task that makes the web request. 
            //The the request returns, the task reads the response stream and extracts the content. 
            //The response body is returned as a string, which becomes available when the task completes.
            // var stringTask = Client.GetStringAsync(uri);

            //Awaits the task for the response string and prints the response to the console.
            // var msg = await stringTask;
            // Console.Write(msg);

            //Same as above but STREAM as opposed to String. Returns response body as a Stream.
            //Steam class: Provides a generic view of a sequence of bytes. This is an abstract class.
            var streamTask = Client.GetStreamAsync(uri); 
            //Repository is a custom type with one property: public string name {get; set;}.
            //var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            /* JSONSERIALISER
                The JSON for a repository object contains dozens of properties, but only the name property 
                will be deserialized. The serializer automatically ignores JSON properties for which there 
                is no match in the target class. This feature makes it easier to create types that work with 
                only a subset of fields in a large JSON packet.
            */
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            foreach (var repo in repositories)
            Console.WriteLine(repo.Name);

            // HttpResponseMessage response = await Client.GetAsync(uri);
            // response.EnsureSuccessStatusCode();
            // string responseBody = await response.Content.ReadAsStringAsync();
            // // Above three lines can be replaced with new helper method below
            // // string responseBody = await client.GetStringAsync(uri);

        }

    }

    internal class Repository
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}