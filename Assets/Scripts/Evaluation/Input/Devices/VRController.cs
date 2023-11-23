using Assets.Scripts.Evaluation.Input.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Evaluation.Gestures.Interface;
using Assets.Scripts.Evaluation.Gestures.ONNX;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public class VRController : IAction, INavigation, IGestureAction
    {
        IVRControllerGestureProvider m_VRControllerGestureProvider;
        public VRController(IVRControllerGestureProvider vrControllerGestureProvider)
        {
            m_VRControllerGestureProvider = vrControllerGestureProvider;
        }

        public bool ActionTriggered()
        {
            //return SteamVR_Actions.default_Trigger.changed && SteamVR_Actions.default_Trigger.state;
            return false;
        }

        public bool OK()
        {
            return m_VRControllerGestureProvider.GetLatestVRControllerGesture() == VRControllerGestureType.U;
        }
        public bool Cancel()
        {
            return m_VRControllerGestureProvider.GetLatestVRControllerGesture() == VRControllerGestureType.LessThan;
        }

        public bool ContextActionTriggered()
        {
            return false;
        }

        public float GetMoveHorizontal()
        {
            //return SteamVR_Actions.default_NavigationXY.axis.x;            
            return 0.0f;
        }

        public float GetMoveVertical()
        {
            //return SteamVR_Actions.default_NavigationXY.axis.y;            
            return 0.0f;
        }

        public float GetMoveHeight()
        {
            //float val = SteamVR_Actions.default_NavigationRYZ.axis.y;
            float val = 0.0f;
            if (Mathf.Abs(val) > 0.5f)
            {
                return val;
            }
            return 0.0f;
        }

        public float GetRotateX()
        {
            //float val = SteamVR_Actions.default_NavigationRYZ.axis.x;
            float val = 0.0f;
            if (Mathf.Abs(val)>0.5f)
            {
                return val * 0.1f;      // slow down hack
            }
            return 0.0f;
        }

        public float GetRotateY()
        {
            return 0.0f;
        }

        public bool Home()
        {
            return m_VRControllerGestureProvider.GetLatestVRControllerGesture() == VRControllerGestureType.INFINITY;
        }

        public bool Win()
        {
            //return SteamVR_Actions.default_Context.changed && SteamVR_Actions.default_Context.state || m_VRControllerGestureProvider.GetLatestVRControllerGesture()==VRControllerGestureType.I;
            return false;
        }

        public bool OpenPalm()
        {
            return false;
        }

        public bool GestureTriggered()
        {
            return m_VRControllerGestureProvider.GetLatestVRControllerGesture() != VRControllerGestureType.None;
        }
    }
}
