namespace worker.powershell.src.Services
{
    public interface ITestService<T>
    {
        void PrintLog(T log);
    }
}