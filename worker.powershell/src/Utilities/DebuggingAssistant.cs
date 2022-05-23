namespace worker.powershell.src.Utilities
{
    ///<Summary>
    /// Enables colour coded terminal output to help visualise the control flow of asynchronous operations during runtime execution.
    ///</Summary>
    public static class DebuggingAssistant
    {
        public static bool? Logging { get; set; }

        public enum MessageType {
            Info,
            Error, 

        }
        public static void LogMessage(Enum? type, string message)
        {
            if (Logging == true)
            {
                switch (type)
                {
                    case MessageType.Info:
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine("INFO: " + message);
                        Console.ForegroundColor = ConsoleColor.White;

                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("EXCEPTION: " + message);
                        Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }
                
        }
   
    }
}