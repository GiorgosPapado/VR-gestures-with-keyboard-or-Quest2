using Assets.Scripts.Evaluation.Gestures.Interface;
using Assets.Scripts.Evaluation.Input.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using Assets.Scripts.Evaluation.Gestures;
using Assets.Scripts.Evaluation.Gestures.Filters;
using Assets.Scripts.Utils;
using Assets.Scripts.Evaluation.Gestures.ONNX;
using Zenject;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public class WebcamHands : IAction, IMouse2D, IGestureAction, IDisposable, ITickable
    {
        private IHandLandmarkProvider m_HandLandmarkProvider;
        private IHandGestureProvider m_HandGestureProvider;

        private CompositeDisposable m_Sub = new CompositeDisposable();
        private Trigger<HandGestureType> m_HandGestureTriggered = new Trigger<HandGestureType>(HandGestureType.None);
        #region Private Methods
        private Vector3 GetEffectivePos(Vector3 pos)
        {
            float offset = (1.0f - EffectiveWebcamAreaFactor) / 2.0f;
            float effectiveX = (pos.x - offset) / EffectiveWebcamAreaFactor;
            float effectiveY = (pos.y - offset) / EffectiveWebcamAreaFactor;

            effectiveX = Mathf.Clamp(effectiveX, 0.0f, 1.0f);
            effectiveY = Mathf.Clamp(effectiveY, 0.0f, 1.0f);
            return new Vector3(effectiveX, effectiveY, pos.z);
        }
        
        #endregion

        #region Properties
        public float EffectiveWebcamAreaFactor { get; set; } = 0.7f;
        public Handedness Handedness { get; set; } = Handedness.RIGHT;
        private Handedness OppositeHandedness
        {
            get
            {
                switch (Handedness)
                {
                    case Handedness.RIGHT:
                        return Handedness.LEFT;                        
                    case Handedness.LEFT:
                        return Handedness.RIGHT;
                    default:
                        return Handedness.LEFT;
                }
            }
        }
        #endregion

        public WebcamHands(IHandLandmarkProvider handLandmarkProvider, IHandGestureProvider handGestureProvider)
        {
            m_HandLandmarkProvider = handLandmarkProvider;
            m_HandGestureProvider = handGestureProvider;
            //m_Sub.Add(handGestureProvider.HandGestureStream.Subscribe(gest => m_HandGestureTriggered.Update(gest)));
        }
        #region ITickable
        public void Tick()
        {
            m_HandGestureTriggered.Update(m_HandGestureProvider.GetLatestHandGesture());
            //Debug.Log("Action Triggered: " + ActionTriggered().ToString());
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            m_Sub.Dispose();
        }
        #endregion

        #region IMouse2D
        public bool Valid
        {
            get
            {
                return m_HandLandmarkProvider.GetLatestHandLandmarks() != null && 
                (m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.Point || m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.PreSelect || m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.Select);
            }
        }
        /*    
        public Vector3 GetMouse2DPosition()
        {
            var hand = m_HandLandmarkProvider.GetLatestHandLandmarks()?.Hands.Where(x=>x.Handedness == this.Handedness).FirstOrDefault(); //hd.Hands.Where(x => x.Handedness == Handedness.RIGHT).FirstOrDefault();
            if (hand != null)
            {
                //var pos1 = hand.Landmarks[(int)HandLandmark.INDEX_FINGET_TIP];
                //var pos2 = hand.Landmarks[(int)HandLandmark.THUMB_TIP];
                var pos1 = hand.Landmarks[(int)HandLandmark.INDEX_FINGER_MCP];
                var pos2 = hand.Landmarks[(int)HandLandmark.THUMB_MCP];

                var pos = (pos1 + pos2) / 2.0f;
                pos = GetEffectivePos(pos);
                pos.y = 1.0f - pos.y;
                var spos = pos * new Vector2(Screen.width, Screen.height);
                return spos;
            }
            else
            {
                return Vector3.zero;
            }
        }*/
        public Vector3 GetMouse2DPosition()
        {
            var hand = m_HandLandmarkProvider.GetLatestHandLandmarks()?.Hands.Where(x => x.Handedness == this.Handedness).FirstOrDefault();
            if (hand != null)
            {
                var pos = hand.Landmarks[(int)HandLandmark.INDEX_FINGER_MCP];

                pos = GetEffectivePos(pos);
                pos.y = 1.0f - pos.y;
                var spos = pos * new Vector2(Screen.width, Screen.height);
                return spos;
            }
            else
            {
                return Vector3.zero;
            }
        }
        #endregion

        #region IAction
        public bool ActionTriggered()
        {
            //Debug.Log("------");
            //Debug.Log("Action Triggered: " + m_HandGestureTriggered.Triggered.ToString());
            //Debug.Log("Last gesture: " + m_HandGestureProvider.GetLatestHandGesture().ToString());
            //bool actionTriggered = m_HandGestureTriggered.Triggered && m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.Select;            
            bool actionTriggered = m_HandGestureTriggered.Triggered && m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.Point;
            return actionTriggered;
        }

        public bool Cancel()
        {
            return m_HandGestureTriggered.Triggered && m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.Back;
        }

        public bool ContextActionTriggered()
        {
            return false;
        }

        public bool OK()
        {
            return m_HandGestureTriggered.Triggered && m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.OK;
        }


        #endregion

        #region IGestureAction
        public bool Home()
        {
            return m_HandGestureTriggered.Triggered && m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.Home;
        }

        public bool Win()
        {
            return m_HandGestureTriggered.Triggered && m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.Win;
        }
        public bool OpenPalm()
        {
            return m_HandGestureTriggered.Triggered && m_HandGestureProvider.GetLatestHandGesture() == HandGestureType.OpenPalm;
        }

        public bool GestureTriggered()
        {
            return m_HandGestureTriggered.Triggered;
        }
        #endregion
    }
}
