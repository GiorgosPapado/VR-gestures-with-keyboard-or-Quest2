using Assets.Scripts.Evaluation.Interactables;
using Assets.Scripts.Evaluation.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input
{
    public class KeyboardMouseWebcamManager : MonoBehaviour
    {
        private InputInterfaceSelector m_InputInterfaceSelector;
        private HandTrackView m_HandTrackView;
        private InteractionInvoker m_InteractionInvoker;

        public GameObject m_HandPointer;
        public GameObject m_GestureViz;

        [Inject]
        public void Init(InputInterfaceSelector inputInterfaceSelector, HandTrackView handTrackView, InteractionInvoker interactionInvoker)
        {
            m_InputInterfaceSelector = inputInterfaceSelector;
            m_HandTrackView = handTrackView;
            m_InteractionInvoker = interactionInvoker;
        }

        public void Update()
        {
            if (m_InputInterfaceSelector.ActiveInputInterfaceName == InputInterfaceName.WebcamGestures || 
                m_InputInterfaceSelector.ActiveInputInterfaceName == InputInterfaceName.ReplayWebcam)
            {
                m_HandTrackView.enabled = true;
                m_InteractionInvoker.Enabled = true;
                m_HandPointer.SetActive(true);
                m_GestureViz.SetActive(true);
            }                
            else
            {
                m_HandTrackView.enabled = false;
                
                m_HandPointer.SetActive(false);
                m_GestureViz.SetActive(false);

                if (m_InputInterfaceSelector.ActiveInputInterfaceName == InputInterfaceName.VRControllers) 
                { 
                    m_InteractionInvoker.Enabled = true;
                }
                else 
                { 
                    m_InteractionInvoker.Enabled = false;
                }
            }
        }
    }
}
