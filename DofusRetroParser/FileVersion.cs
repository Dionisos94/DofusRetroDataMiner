using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRetroParser
{
    public class FileVersion
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }

        public FileVersion(string versionStr) //i.e items,fr,100323
        {
            var split = versionStr.Split(',');
            
            if(split.Length != 3)
            {
                throw new Exception($"Impossible to parse '' into a FileVersion");
            }
            Name = split[0];
            Language = split[1];
            Version = split[2];
        }

        public string GetFileName()
        {
            return $"{Name}_{Language}_{Version}.swf";
        }
    }
}
