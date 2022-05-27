namespace worker.powershell.src.Utilities
{
    ///<Summary>
    /// returns a string representation of text in file. Provides basic validation to ensure path points to a file.
    ///</Summary>
   public static class ScriptParser
   {
       public static string GetScriptFromPath(string path)
       {
           string scriptFileLines = Path.HasExtension(path) ? File.ReadAllText(path) : "Ivalid script source";
           return scriptFileLines;
       }
   }
}
