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

        public static bool CheckIfVersionIsDifferent(string[] versions)
        {
            var versionPath = $"D:\\Downloads\\exports\\versions\\versions_{Config.Language}.txt";
            var actualVersions = versions.ToDictionary(x => x.Split(",")[0], x => x.Split(",")[2]);
            var isDifferent = false;
            if (File.Exists(versionPath))
            {
                var content = File.ReadAllText(versionPath);
                var savedVersions = content.Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToDictionary(x => x.Split(",")[0], x => x.Split(",")[2].Replace("\r", string.Empty));

                if(savedVersions.Count() != actualVersions.Count) { Console.WriteLine("Different version file detected."); isDifferent = true; }

                foreach(var version in actualVersions.Keys) 
                {
                    if (!savedVersions.ContainsKey(version))
                    {
                        Console.WriteLine($"Different version detected for key '{version}': Actual '{savedVersions[version]}'; Saved Not Found");
                        isDifferent = true;
                    }

                    if (actualVersions[version] != savedVersions[version])
                    {
                        Console.WriteLine($"Different version detected for key '{version}': Actual '{savedVersions[version]}'; Saved '{actualVersions[version]}'");
                        isDifferent = true;
                    }
                }
            }
            else
            {
                isDifferent = true;
            }


            if (isDifferent)
            {
                Console.WriteLine("Saving version file");
                SaveVersionFile(versions);
            }
            return isDifferent;
        }

        private static void SaveVersionFile(string[] versions)
        {
            var versionPath = $"D:\\Downloads\\exports\\versions\\versions_{Config.Language}.txt";
            var oldVersionPath = $"D:\\Downloads\\exports\\versions\\versions_{Config.Language}_old.txt";

            if (File.Exists(versionPath))
            {
                File.Move(versionPath, oldVersionPath, true);
            }

            var sb = new StringBuilder();
            foreach (var version in versions)
            {
                sb.AppendLine(version);
            }
            File.WriteAllText(versionPath, sb.ToString());
        }
    }
}
