using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using Zenject;
using Assets.Scripts.Evaluation.Input.Interface;
using Assets.Scripts.Evaluation.Gestures.Interface;
using TMPro;
using Assets.Scripts.Evaluation.Gestures.ONNX;
using UnityEngine.UI;
using Assets.Scripts.Evaluation.Gestures.Filters;

namespace Assets.Scripts.Evaluation.Views
{
    class VRGestureVisualizer : MonoBehaviour
    {        
        public Color m_GestureNoneColor;
        public Color m_GestureTriggeredColor;

        private IGestureAction m_GestureAction;
        private IVRControllerGestureProvider m_VRControllerGestureProvider;

        public TextMeshProUGUI m_GestureText;
        public Image m_Indicator;

        private bool m_InTrigger = false;
        private IDisposable m_Sub = null;

        [Inject]
        void Init(IGestureAction gestureAction, IVRControllerGestureProvider vrControllerGestureProvider)
        {
            m_GestureAction = gestureAction;
            m_VRControllerGestureProvider = vrControllerGestureProvider;
        }
       
        public void Update()
        {
            if (m_GestureAction.GestureTriggered())
            {

                if (m_VRControllerGestureProvider.GetLatestVRControllerGesture() != VRControllerGestureType.None)
                {
                    m_InTrigger = true;
                    m_Indicator.color = m_GestureTriggeredColor;

                    m_Sub?.Dispose();

                    m_Sub = Observable.Timer(TimeSpan.FromMilliseconds(1500)).Subscribe(_ =>
                    {
                        m_InTrigger = false;
                    });

                    switch (m_VRControllerGestureProvider.GetLatestVRControllerGesture())
                    {
                        case VRControllerGestureType.LessThan:
                            m_GestureText.text = "Back";
                            break;
                        case VRControllerGestureType.U:
                            m_GestureText.text = "OK";
                            break;
                        case VRControllerGestureType.INFINITY:
                            m_GestureText.text = "Home";
                            break;                        
                        case VRControllerGestureType.None:
                            m_GestureText.text = "None";
                            break;
                        case VRControllerGestureType.I:
                            m_GestureText.text = "Entity HUD";
                            break;
                    }
                }
            }
            
            if (!m_InTrigger)
            {
                m_Indicator.color = m_GestureNoneColor;
                m_GestureText.text = "None";
            }
        }

        public void OnDisable()
        {
            m_GestureText.text = "None";
        }
    }
}
