using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DofusRetroParser
{
    public class FileManager
    {
        public static void ReNewDecompDirectory()
        {
            if (Directory.Exists(Config.LangFolder))
            {
                Directory.Delete(Config.LangFolder, true);
            }

            Directory.CreateDirectory(Config.ScriptsFolder);
            Directory.CreateDirectory(Config.SWFFolder);
        }

        public static void MoveScript(string currentFile)
        {
            var currentScriptFilePath = Path.Combine(Config.ScriptsFolder, currentFile);
            Directory.Move(Config.ExtractedScriptFolder, currentScriptFilePath);
            File.Move(Path.Combine(currentScriptFilePath, "DoAction.as"), Path.Combine(currentScriptFilePath, "DoAction_1.as"));
        }

        public static void WriteAllScriptsToTxt(string currentFile)
        {
            var sb = new StringBuilder();
            foreach(var file in Directory.GetFiles(Path.Combine(Config.ScriptsFolder, currentFile)).ToArray())
            {
                sb.Append(File.ReadAllText(file));
            }
            File.WriteAllText(Path.Combine(Config.LangFolder, $"{currentFile}.txt"), sb.ToString());
        }
    }
}
