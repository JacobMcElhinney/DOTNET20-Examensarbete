namespace worker.powershell
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
        
    }
}