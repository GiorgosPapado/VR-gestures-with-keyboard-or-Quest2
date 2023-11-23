using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    public class FileNameGenerator
    {
        public FileNameGenerator()
        {

        }

        public FileNameGenerator(string fileNamePrefix, string fileNameSuffix)
        {
            FileNamePrefix = fileNamePrefix;
            FileNameSuffix = fileNameSuffix;
        }

        public string FileNamePrefix { get; set; } = string.Empty;
        public string FileNameSuffix { get; set; } = string.Empty;
        public string Generate()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string id = Guid.NewGuid().ToString().Substring(0, 4);
            return FileNamePrefix + "-" + timestamp + "-" + id + FileNameSuffix;
        }
    }
}
