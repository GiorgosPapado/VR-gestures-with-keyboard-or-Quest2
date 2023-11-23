using Assets.Scripts.Evaluation.Gestures.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using UniRx;

namespace Assets.Scripts.Evaluation.Input.Replay.Webcam
{
    public class WebcamStateCapturer : IStateCapturer
    {
        private KeyboardMouseInputRecorder m_KeyboardMouseRecorder;
        private IHandLandmarkProvider m_HandLandmarkProvider;
        private IHandGestureProvider m_HandGestureProvider;
        private InputRecorder m_InputRecorder;
        private List<WebcamAppState> m_States = new List<WebcamAppState>();

        public WebcamStateCapturer(KeyboardMouseInputRecorder keyMouseInputRecorder, IHandLandmarkProvider handLandmarkProvider,
            IHandGestureProvider handGestureProvider, InputRecorder inputRecorder)
        {
            m_KeyboardMouseRecorder = keyMouseInputRecorder;
            m_HandLandmarkProvider = handLandmarkProvider;
            m_HandGestureProvider = handGestureProvider;
            m_InputRecorder = inputRecorder;
        }

        private void SavePart(string fname, bool threaded)
        {
            if(threaded)
            {
                UniRx.Observable.Return(m_States).ObserveOn(Scheduler.ThreadPool)
                        .Subscribe(states =>
                        {
                            try
                            {
                                using (var fs = new FileStream(fname, FileMode.CreateNew))
                                {
                                    MessagePack.MessagePackSerializer.Serialize(fs, states);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.Log(ex);
                            }
                        });
            }
            else
            {
                try
                {
                    using (var fs = new FileStream(fname, FileMode.CreateNew))
                    {
                        MessagePack.MessagePackSerializer.Serialize(fs, m_States);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            }
        }

        public void CaptureState()
        {
            var keyboardstate = m_KeyboardMouseRecorder.GetState();
            var landmarks = m_HandLandmarkProvider.GetLatestHandLandmarks();
            var handgesture = m_HandGestureProvider.GetLatestHandGesture();
            var input = m_InputRecorder.GetState();

            WebcamAppState wcstate = new WebcamAppState();
            wcstate.Time = Time.time;
            wcstate.KeyboardMouseState = keyboardstate;
            wcstate.HandLandmarks = landmarks;
            wcstate.LatestHandGesture = handgesture;
            wcstate.InputState = input;
            m_States.Add(wcstate);
        }

        public void FlushState(string filePath, bool threaded)
        {
            SavePart(filePath,threaded);
            m_States = new List<WebcamAppState>();
        }

        public int BufferSize => m_States.Count;
    }
}
