using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input.Interface
{
    public interface IMouse2D
    {
        public Vector3 GetMouse2DPosition();
        public bool Valid { get; }
    }
}
