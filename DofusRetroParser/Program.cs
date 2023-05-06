// See https://aka.ms/new-console-template for more information

using DofusRetroParser;
using System.Diagnostics;


FileManager.ReNewDecompDirectory();
var dataGrabber = new DataGrabber();
var versions = dataGrabber.GetFileVersions();

foreach (string vers in versions)
{
    var fileVersion = new FileVersion(vers);
    var outputPath = Path.Combine(Config.SWFFolder, $"{fileVersion.Name}.swf");
    dataGrabber.DownloadFile(fileVersion.GetFileName(), outputPath);
    using (var process = Process.Start(Config.GetProcessInfo($"ffdec.bat -selectclass DoAction -export script {Config.DecompFolder} {outputPath}")))
    {
        process?.WaitForExit();
    }

    FileManager.MoveScript(fileVersion.Name);
    FileManager.WriteAllScriptsToTxt(fileVersion.Name);
}

// Heavily Inspired from
// https://github.com/Thomas-Anonymous/CommunityTools/blob/c1a2e84ac8230e2af712616dd32c0723bd812486/Config.cs
// FFDEC is required to parse .swf files