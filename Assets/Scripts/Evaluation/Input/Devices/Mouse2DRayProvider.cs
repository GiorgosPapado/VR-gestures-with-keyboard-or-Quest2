using Assets.Scripts.Evaluation.Input.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public class Mouse2DRayProvider : MonoBehaviour, IRayProvider
    {
        public  Camera m_Camera;
        private IMouse2D m_Mouse;
        [Inject]
        public void Init(IMouse2D mouse)
        {
            m_Mouse = mouse;
        }
        public Ray GetRay()
        {            
            if (m_Mouse.Valid)
            {
                Vector3 mousepos = m_Mouse.GetMouse2DPosition();
                return m_Camera.ScreenPointToRay(mousepos);
            }
            else
            {
                return new Ray();
            }
        }
    }
}
