using Assets.Scripts.Evaluation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Evaluation.Views
{
    public class VRGesturesHelp : MonoBehaviour
    {
        private TopWidgetTracker m_TopWidgetTracker;
        public GameObject m_VRGesturesHelp;
        [Inject]
        public void Init(TopWidgetTracker topWidgetTracker)
        {
            m_TopWidgetTracker = topWidgetTracker;
        }
        public void Update()
        {
            //if(SteamVR_Actions.default_Help.changed && SteamVR_Actions.default_Help.state)
            if(false)
            {
                if (m_VRGesturesHelp.activeSelf)
                {
                    m_TopWidgetTracker.PopWidget(m_VRGesturesHelp);
                }
                else
                {
                    m_TopWidgetTracker.PushWidget(m_VRGesturesHelp);
                }
            }
            
        }
    }
}
