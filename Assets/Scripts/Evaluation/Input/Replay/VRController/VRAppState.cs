using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Evaluation.Gestures.ONNX;
using Assets.Scripts.Evaluation.Input.VRController;
using MessagePack;

namespace Assets.Scripts.Evaluation.Input.Replay.VRController
{
    [MessagePackObject]
    public class VRAppState
    {
        [Key("Time")]
        public float Time { get; set; }
        [Key("InputState")]
        public InputState InputState { get; set; }        
        [Key("SensorData")]
        public SensorDataBatch SensorData { get; set; }
        [Key("KeyboardMouseState")]
        public KeyboardMouseState KeyboardMouseState { get; set; }
        [Key("RecordingGesture")]
        public bool RecordingGesture { get; set; }
        [Key("VRControllerGesture")]
        public VRControllerGestureType VRControllerGesture { get; set; }
        [Key("HelpButtonDown")]
        public bool HelpButtonDown { get; set; }
        [Key("ContextButtonDown")]
        public bool ContextButtonDown { get; set; }
    }
}
