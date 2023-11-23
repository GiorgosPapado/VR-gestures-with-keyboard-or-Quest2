using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Evaluation.Model.Twitter
{
    public class TweetInfo : IRepositoryItem
    {
        public int ID { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Today;
        public string Tweet { get; set; } = string.Empty;
        public IList<string> Hashtags { get; set; } = new List<string>();
        public string IP { get; set; }
    }
}
