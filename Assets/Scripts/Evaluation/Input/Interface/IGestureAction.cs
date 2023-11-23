using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Input.Interface
{
    public interface IGestureAction
    {
        public bool Home();
        public bool Win();
        public bool OpenPalm();
        public bool GestureTriggered();
    }
}
