using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Input.Interface
{
    public interface INavigation
    {
        public float GetMoveHorizontal();
        public float GetMoveVertical();
        public float GetMoveHeight();
        public float GetRotateX();
        public float GetRotateY();
    }
}
