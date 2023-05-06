using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DofusRetroParser
{
    public class Config
    {
        private static string FFDEC = @"C:\Program Files (x86)\FFDec";
        private static string CMD = @"C:\Windows\system32\cmd.exe";
        public static string DecompFolder = @"D:\Downloads\exports\";
        public static bool IsTemporis = true;
        public static string Language = "fr";
        

        public static ProcessStartInfo GetProcessInfo(string arg)
        {
            return new ProcessStartInfo()
            {
                WorkingDirectory = FFDEC,
                FileName = CMD,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = false,
                RedirectStandardOutput = false,
                Arguments = "/C " + arg + " & exit",
            };
        }

        public static string LangFolder => Path.Combine(DecompFolder, "lang");
        public static string ScriptsFolder => Path.Combine(LangFolder, "scripts");
        public static string SWFFolder => Path.Combine(LangFolder, "SWF");
        public static string ExtractedScriptFolder => Path.Combine(DecompFolder, "scripts", "frame_1");
        public static string AnkamaServerBaseUrl => "http://dofusretro.cdn.ankama.com/" + (IsTemporis ? "temporis" : string.Empty);
        public static string AnkamaServerLangUrl => Path.Combine(AnkamaServerBaseUrl, "lang");
        public static string AnkamaServerSwfUrl => Path.Combine(AnkamaServerLangUrl, "swf");
        public static string AnkamaServerVersionFile => Path.Combine(AnkamaServerLangUrl, $"versions_{Language}.txt");
    }
}
