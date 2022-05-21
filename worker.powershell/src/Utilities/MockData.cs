using worker.powershell.src.Models;

namespace worker.powershell.src.Utilities
{
    /// <summary>
    /// Provides mock data for testing during development phase.
    /// </summary>
    public static class MockData
    {
        private static string scriptPath = @"TestScript.ps1";
        public static string ScriptPath
        {
            get { return scriptPath; }
            set { scriptPath = value; }
        }

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
                TimeStamp = DateTime.Now,
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

    }
}