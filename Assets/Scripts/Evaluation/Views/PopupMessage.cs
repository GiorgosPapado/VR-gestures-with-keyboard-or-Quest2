using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assets.Scripts.View
{
    public class PopUpMessage
    {
        public string Message = "";
        public float MinDuration = 0.0f;
        public bool KeepColorAfterDuration = false;
        public PopUpMessage(string message, float dur, bool keepColorAfterDuration)
        {
            Message = message;
            MinDuration = dur;
            KeepColorAfterDuration = keepColorAfterDuration;
        }

        public PopUpMessage(string message, float dur)
        {
            Message = message;
            MinDuration = dur;
        }

        public PopUpMessage(string message)
        {
            Message = message;
            MinDuration = 3.0f;
        }
    }
}
