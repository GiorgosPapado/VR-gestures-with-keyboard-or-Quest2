using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Actions
{
    public class ShowVideoAction
    {
        public int ID { get; set; }
        public string Tag;
        public ShowVideoAction(int ID)
        {
            this.ID = ID;
        }
    }
}
