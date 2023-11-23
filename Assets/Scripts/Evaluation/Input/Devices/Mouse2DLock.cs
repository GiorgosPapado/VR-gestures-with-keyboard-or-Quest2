using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Evaluation.Input.Interface;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public class Mouse2DLock : IInitializable
    {
        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
