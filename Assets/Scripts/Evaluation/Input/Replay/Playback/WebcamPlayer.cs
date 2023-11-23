using Assets.Scripts.Evaluation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using System.IO;
using Assets.Scripts.Evaluation.Input.Interface;
using UnityEngine;
using UniRx;

namespace Assets.Scripts.Evaluation.Input.Replay.Playback
{
    public class WebcamPlayer : Activatable, ITickable, IAction, IGestureAction, IMouse2D, INavigation, IPlayer
    {        
        private List<string> m_FileParts = new List<string>();
        private int m_CurrentPartID = -1;
        private List<WebcamAppState> m_CurrentPartStates;
        private int m_CurrentFrameID = -1;
        private Subject<Unit> m_OnPlaybackFinishedSubject = new Subject<Unit>();
        private float m_RecordingStartTime;
        private float m_CurrentStartTime;
        private bool m_TriggerFrame;
        public WebcamPlayer()
        {

        }

        private bool LoadNextPart()
        {
            m_CurrentPartID++;
            m_CurrentFrameID = 0;
            if (m_CurrentPartID < m_FileParts.Count)
            {
                using(var fs = new FileStream(m_FileParts[m_CurrentPartID],FileMode.Open))
                {
                    m_CurrentPartStates = MessagePack.MessagePackSerializer.Deserialize<List<WebcamAppState>>(fs);                    
                }
                if (m_CurrentPartStates.Count > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        private bool LoadNextPartIfNeeded()
        {
            if (m_CurrentFrameID == m_CurrentPartStates.Count-1)
                return LoadNextPart(); // return false if no more parts available
            return true;        
        }
        protected override void OnEnable()
        {
            m_FileParts.Clear();
            m_CurrentPartID = -1;
            m_CurrentFrameID = 0;
            m_TriggerFrame = true;
            m_FileParts.AddRange(Directory.GetFiles(SessionPath));
            if (!LoadNextPart())
                Enabled = false;
            else
            {
                m_CurrentStartTime = Time.time;
                m_RecordingStartTime = m_CurrentPartStates.First().Time;
            }
        }

        private void ApplyState()
        {

        }

        protected override void OnDisable()
        {
            m_CurrentFrameID = -1;
            m_CurrentPartID = -1;
            m_OnPlaybackFinishedSubject.OnNext(Unit.Default);
        }
        public void Tick()
        {
            if (!Enabled)
                return;
            if (!LoadNextPartIfNeeded())
            { 
                Enabled = false;
            }
            else
            {
                ApplyState();
                if (Time.time - m_CurrentStartTime >= m_CurrentPartStates[m_CurrentFrameID].Time - m_RecordingStartTime)
                {
                    m_TriggerFrame = true;
                    m_CurrentFrameID++;
                }
                else
                    m_TriggerFrame = false;
                
            }
        }
        #region IAction
        public bool ActionTriggered()
        {
            if(Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.ActionTriggered && m_TriggerFrame;
            }
            return false;
        }
        public bool ContextActionTriggered()
        {
            if (Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.ContextActionTriggered && m_TriggerFrame;
            }
            return false;
        }
        public bool OK()
        {
            if (Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.OK && m_TriggerFrame;
            }
            return false;
        }
        public bool Cancel()
        {
            if (Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.Cancel && m_TriggerFrame;
            }
            return false;
        }
        #endregion
        #region IGestureAction
        public bool Home()
        {
            if(Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.Home && m_TriggerFrame;
            }
            return false;
        }
        public bool Win()
        {
            if(Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.Win && m_TriggerFrame;
            }
            return false;
        }
        public bool OpenPalm()
        {
            if(Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.OpenPalm && m_TriggerFrame;
            }
            return false;
        }
        public bool GestureTriggered()
        {
            if(Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.GestureTriggered && m_TriggerFrame;
            }
            return false;
        }
        #endregion
        #region IMouse2D
        public Vector3 GetMouse2DPosition()
        {
            if(Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.Mouse2DPosition;
            }
            return Vector3.zero;
        }
        public bool Valid
        {
            get
            {
                if(Enabled)
                {
                    return m_CurrentPartStates[m_CurrentFrameID].InputState.Valid;
                }
                return false;
            }
        }
        #endregion
        #region INavigation
        public float GetMoveHorizontal()
        {
            if (Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.MoveHorizontal;
            }
            return 0.0f;
        }
        public float GetMoveVertical()
        {
            if (Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.MoveVertical;
            }
            return 0.0f;
        }
        public float GetMoveHeight()
        {
            if (Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.MoveHeight;
            }
            return 0.0f;
        }
        public float GetRotateX()
        {
            if (Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.RotateX;
            }
            return 0.0f;
        }
        public float GetRotateY()
        {
            if (Enabled)
            {
                return m_CurrentPartStates[m_CurrentFrameID].InputState.RotateY;
            }
            return 0.0f;
        }
        #endregion
        public string SessionPath { get; set; } = ".\\webcam - session-1-2021-09-22-17-07-57-1be2";

        #region IPlayer
        public IObservable<Unit> OnPlaybackFinished => m_OnPlaybackFinishedSubject.AsObservable();
        #endregion
    }
}
