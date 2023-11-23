using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Actions
{
    public class ShowTweetInfoAction
    {
        public int ID { get; private set; }
        public ShowTweetInfoAction(int ID)
        {
            this.ID = ID;
        }
    }
}
