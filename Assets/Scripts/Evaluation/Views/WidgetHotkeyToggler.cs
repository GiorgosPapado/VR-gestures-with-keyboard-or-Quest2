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
    public class WidgetHotkeyToggler : MonoBehaviour
    {
        public KeyCode[] m_KeyCodes;
        public GameObject[] m_GameObjects;
        private TopWidgetTracker m_TopWidgetTracker;
        [Inject]
        public void Init(TopWidgetTracker widgetTracker)
        {
            m_TopWidgetTracker = widgetTracker;
        }
        public void Update()
        {
            for(int i=0;i<m_KeyCodes.Length; i++)
            {
                if(UnityEngine.Input.GetKeyDown(m_KeyCodes[i]))
                {
                    m_TopWidgetTracker.PushWidget(m_GameObjects[i]);
                }
            }
        }
    }
}
