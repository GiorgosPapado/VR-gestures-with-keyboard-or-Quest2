using Assets.Scripts.Evaluation.Input.VRController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input.Replay.VRController
{
    public class VRControllerStateCapturer : IStateCapturer
    {
        private List<VRAppState> m_States = new List<VRAppState>();
        public int BufferSize => m_States.Count;
        private SensorTracker m_SensorTracker;
        private KeyboardMouseInputRecorder m_KeyboardMouseRecorder;
        private InputRecorder m_InputRecorder;
        private VRGestureRecordTrigger m_VRGestureRecordTrigger;
        private bool m_RecordingGesture = false;
        private VRGestureProvider m_VRGestureProvider;
        public VRControllerStateCapturer(KeyboardMouseInputRecorder keyMouseInputRecorder,
            InputRecorder inputRecorder, SensorTracker sensorTracker, VRGestureRecordTrigger vrGestureRecordTrigger, VRGestureProvider vrGestureProvider)
        {
            m_KeyboardMouseRecorder = keyMouseInputRecorder;
            m_InputRecorder = inputRecorder;
            m_SensorTracker = sensorTracker;
            m_VRGestureRecordTrigger = vrGestureRecordTrigger;
            m_VRGestureProvider = vrGestureProvider;
            m_VRGestureRecordTrigger.OnGestureRecordStart.Subscribe(_ => m_RecordingGesture = true);
            m_VRGestureRecordTrigger.OnGestureRecordStop.Subscribe(_ => m_RecordingGesture = false);
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
            var input = m_InputRecorder.GetState();
            var sensordata = m_SensorTracker.GetLatestSensorData();
            VRAppState state = new VRAppState();
            state.Time = Time.time;
            state.KeyboardMouseState = keyboardstate;
            state.InputState = input;
            state.SensorData = sensordata;
            state.RecordingGesture = m_RecordingGesture;
            state.VRControllerGesture = m_VRGestureProvider.GetLatestVRControllerGesture();
            //state.HelpButtonDown = SteamVR_Actions.default_Help.state;
            //state.ContextButtonDown = SteamVR_Actions.default_Help.state;
            state.HelpButtonDown = false;
            state.ContextButtonDown = false;
            m_States.Add(state);
        }

        public void FlushState(string filePath, bool threaded)
        {
            SavePart(filePath, threaded);
            m_States = new List<VRAppState>();
        }
    }
}
