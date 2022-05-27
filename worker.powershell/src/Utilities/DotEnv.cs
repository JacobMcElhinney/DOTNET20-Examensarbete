// namespace worker.powershell.src.Utilities
// {
//     using System;
//     using System.IO;

//     /// <summary>
//     /// Enables alternative .env over appsettings.json.
//     /// </summary>

//     //Conforming to convention of reliance on .env files as in related microservices
//     public static class DotEnv
//     {
//         public static void Load(string filePath)
//         {
//             if (!File.Exists(filePath)) return;

//             foreach (string line in File.ReadAllLines(filePath))
//             {
//                 string[] subStrings = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

//                 if (subStrings.Length != 2) continue;

//                 Environment.SetEnvironmentVariable(subStrings[0], subStrings[1]);
//             }

//         }

//         //THE FOLLOWING CODE NEEDS TO BE INSERTED AT THE TOP OF PROGRAM.CS
//         // string root = Directory.GetCurrentDirectory();
//         // string dotenv = Path.Combine(root, ".env");
//         // DotEnv.Load(dotenv);
//         // var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
//     }
// }