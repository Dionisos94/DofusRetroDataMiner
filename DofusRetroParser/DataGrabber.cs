using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DofusRetroParser
{
    public class DataGrabber
    {
        private WebClient _client;

        public DataGrabber()
        {
            _client = new WebClient();
        }

        public DataGrabber(WebClient client) 
        {
            _client = client;
        }

        public string[] GetFileVersions()
        {
            string versionFileContent = _client.DownloadString(Config.AnkamaServerVersionFile);
            return versionFileContent.Split('=')[1].Split('|').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        }

        public void DownloadFile(string fileName, string path)
        {
            _client.DownloadFile(Path.Combine(Config.AnkamaServerSwfUrl,fileName), path);

        }
    }
}
