using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;
namespace Assets.Scripts.Evaluation.Model.Facebook
{
    public class FbInfo : IRepositoryItem
    {
        public int ID { get; set; }
        public string Username {get; set;}
        public DateTime Date { get; set; } = DateTime.Today;
        public string Post { get; set; }
        public IList<string> Hashtags { get; set; } = new List<string>();
        public string IP { get; set; }
    }
}
