using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using Assets.Scripts.Evaluation.Gestures;
using Assets.Scripts.Evaluation.Gestures.ONNX;

namespace Assets.Scripts.Evaluation.Input.Replay
{
    [MessagePackObject]
    public class WebcamAppState
    {
        [Key("Time")]
        public float Time { get; set; }
        [Key("InputState")]
        public InputState InputState { get; set; }
        [Key("HandData")]
        public HandData HandLandmarks { get; set; }
        [Key("LatestHandGesture")]
        public HandGestureType LatestHandGesture { get; set; }
        [Key("KeyboardMouseState")]
        public KeyboardMouseState KeyboardMouseState { get; set; }
    }
}
