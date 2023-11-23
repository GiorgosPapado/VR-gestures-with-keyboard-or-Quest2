using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Input.Interface
{
    public interface IAction
    {
        public bool ActionTriggered();
        public bool ContextActionTriggered();
        public bool OK();
        public bool Cancel();
    }
}
