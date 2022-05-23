namespace worker.powershell.src.Utilities
{
   public static class ScriptParser
   {
       public static string GetScriptFromPath(string path)
       {
           string scriptFileLines = Path.HasExtension(path) ? File.ReadAllText(path) : "Ivalid script source"; //! Replace with powershell exception?
           return scriptFileLines;
       }
   }
}
