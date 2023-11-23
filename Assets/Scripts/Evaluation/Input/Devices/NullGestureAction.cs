using Assets.Scripts.Evaluation.Input.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public class NullGestureAction : IGestureAction
    {
        public bool GestureTriggered()
        {
            return false;
        }

        public bool Home()
        {
            return false;
        }

        public bool OpenPalm()
        {
            return false;
        }

        public bool Win()
        {
            return false;
        }
    }
}
