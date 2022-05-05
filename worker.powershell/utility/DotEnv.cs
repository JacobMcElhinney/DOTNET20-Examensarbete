namespace worker.powershell
{
    using System;
    using System.IO;

    //Conforming to convention of reliance on .env files as in related microservices
    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath)) return;

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] subStrings = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (subStrings.Length != 2) continue;

                Environment.SetEnvironmentVariable(subStrings[0], subStrings[1]);
            }


        }
    }
}