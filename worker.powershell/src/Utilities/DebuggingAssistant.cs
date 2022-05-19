namespace worker.powershell.src.Utilities
{
    ///<Summary>
    /// Enables colour coded terminal output to help visualise the control flow of asynchronous operations during runtime execution.
    ///</Summary>
    public static class DebuggingAssistant
    {

        public enum MessageType {
            Info,
            Error, 

        }
        public static void LogMessage(Enum? type, string message)
        {
            switch (type)
            {
                case MessageType.Info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("INFO: " + message);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("TEST: " + message);
                break;
            }
        }
   
    }
}