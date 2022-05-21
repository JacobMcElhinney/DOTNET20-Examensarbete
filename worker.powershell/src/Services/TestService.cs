using worker.powershell.src.Models;

namespace worker.powershell.src.Services
{
    public class TestService : ITestService<Log>
    {
        public void PrintLog(Log log)
        {
            log.Message = "this log was resolved from the DI container";
            System.Console.WriteLine($"got it..{log.Message}");
        }
    }
}