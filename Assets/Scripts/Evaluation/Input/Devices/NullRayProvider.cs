using Assets.Scripts.Evaluation.Input.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public class NullRayProvider : IRayProvider
    {
        public Ray GetRay()
        {
            return new Ray();
        }
    }
}
