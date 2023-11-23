using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Evaluation.Input.Replay
{
    public class FileManager
    {
        public string CreateDirectory(string name)
        {
            var dirNameGen = new FileNameGenerator(name, "");
            string dirname = dirNameGen.Generate();
            if (!Directory.Exists(dirname)) { 
                Directory.CreateDirectory(dirname);                
            }
            return dirname;
        }
    }
}
