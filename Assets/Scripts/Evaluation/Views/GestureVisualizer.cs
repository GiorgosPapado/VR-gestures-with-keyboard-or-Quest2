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
    class GestureVisualizer : MonoBehaviour
    {
        public Color m_GestureRecognizedColor;
        public Color m_GestureNotRecognizedColor;
        public Color m_GestureTriggeredColor;        

        private IGestureAction m_GestureAction;
        private IHandGestureProvider m_HandGestureProvider;

        public TextMeshProUGUI m_GestureText;
        public Image m_Indicator;      
              
        private bool m_InTrigger = false;

        [Inject]
        void Init(IGestureAction gestureAction, IHandGestureProvider handGestureProvider)
        {
            m_GestureAction = gestureAction;
            m_HandGestureProvider = handGestureProvider;
        }

        private bool IsGestureNone()
        {
            switch(m_HandGestureProvider.GetLatestHandGesture())
            {
                case HandGestureType.Negative:
                case HandGestureType.Select:
                case HandGestureType.None:
                    return true;
                default:
                    return false;
            }
        }

        public void Update()
        {
            if(m_GestureAction.GestureTriggered())
            {

                if (IsGestureNone()) 
                { 
                    m_Indicator.color = m_GestureNotRecognizedColor;
                }
                else
                {
                    if(m_HandGestureProvider.GetLatestHandGesture() != HandGestureType.PreSelect)
                    {
                        m_InTrigger = true;
                        m_Indicator.color = m_GestureTriggeredColor;

                        Observable.Timer(TimeSpan.FromMilliseconds(500)).Subscribe(_ =>
                        {
                            m_InTrigger = false;
                        });
                    }
                }
            }
            switch(m_HandGestureProvider.GetLatestHandGesture())
            {
                case HandGestureType.Back:
                    m_GestureText.text = "Back";
                    break;
                case HandGestureType.OK:
                    m_GestureText.text = "OK";
                    break;
                case HandGestureType.Home:
                    m_GestureText.text = "Home";
                    break;
                case HandGestureType.OpenPalm:
                    m_GestureText.text = "Open Palm";
                    break;
                case HandGestureType.PreSelect:
                    m_GestureText.text = "Pre-select";
                    break;
                case HandGestureType.Point:
                    m_GestureText.text = "Select";
                    break;
                case HandGestureType.Select:
                    //m_GestureText.text = "Select2";
                    //break;
                case HandGestureType.Negative:
                    //m_GestureText.text = "Negative";
                    //break;
                case HandGestureType.None:
                    m_GestureText.text = "None";
                    break;
                case HandGestureType.Win:
                    m_GestureText.text = "Win";
                    break;
            }
            if (!m_InTrigger)
            {
                if (IsGestureNone())
                    m_Indicator.color = m_GestureNotRecognizedColor;
                else
                {
                    m_Indicator.color = m_GestureRecognizedColor;
                }                
            }
        }

        public void OnDisable()
        {
            m_GestureText.text = "None";
        }
    }
}
