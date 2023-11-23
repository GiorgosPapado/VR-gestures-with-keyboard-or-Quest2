using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Actions
{
    public class ShowInfoOnHUDAction
    {
        public string InfoText { get; set; }
        public float Duration { get; set; } = 0.0f;
        public ShowInfoOnHUDAction(string infotext, float duration = 0.0f)
        {
            this.InfoText = infotext;
            this.Duration = duration;
        }
    }
}
