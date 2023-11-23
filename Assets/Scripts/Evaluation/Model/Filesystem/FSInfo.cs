using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Model.Filesystem
{
    public enum FSFileType
    {
        PDF,
        Photo,
        Video,        
        CSV
    }
    public class FSInfo : IRepositoryItem
    {
        public int ID { get; set; }
        public string Filename { get; set; }
        public FSFileType Filetype { get; set; }
    }
}
