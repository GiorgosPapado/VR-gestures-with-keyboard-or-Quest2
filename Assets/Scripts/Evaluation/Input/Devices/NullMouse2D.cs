using Assets.Scripts.Evaluation.Input.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public class NullMouse2D : IMouse2D
    {
        public bool Valid => false;

        public Vector3 GetMouse2DPosition()
        {
            return Vector3.zero;
        }
    }
}
