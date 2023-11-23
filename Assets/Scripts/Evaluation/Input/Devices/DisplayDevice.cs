using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public class DisplayDevice : MonoBehaviour, IInitializable
    {
        public GameObject m_MonitorDevice;
        public GameObject m_VRDevice;
        private DisplayDeviceType m_DisplayDeviceType;

        [Inject]
        public void Init(DisplayDeviceType devType)
        {
            m_DisplayDeviceType = devType;    
        }

        public void Initialize()
        {
            switch (m_DisplayDeviceType)
            {
                case DisplayDeviceType.Monitor:
                    m_MonitorDevice.SetActive(true);
                    break;
                case DisplayDeviceType.VR:
                    m_VRDevice.SetActive(true);
                    break;
            }
        }
    }
}
