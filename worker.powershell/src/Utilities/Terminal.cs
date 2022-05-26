namespace worker.powershell.src.Utilities
{
    ///<Summary>
    /// Enables colour coded terminal output to help visualise the control flow of asynchronous operations during runtime execution. Set "WorkerOptions:Logging" to true/fale to turn terminal logging on/off. 
    ///</Summary>
    public static class Terminal
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
                        System.Console.WriteLine("task: " + message);
                        Console.ForegroundColor = ConsoleColor.White;

                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine(" [!]: " + message);
                        Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }
        }
   
    }
}