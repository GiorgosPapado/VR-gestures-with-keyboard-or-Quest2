using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Model
{
    [Flags]
    public enum ControllerState
    {
        None = 0,
        BUTTON1_PRESSED = 1,
        BUTTON2_PRESSED = 2,
        BUTTON3_PRESSED = 3
    }

    [MessagePackObject]
    [Union(0, typeof(ControllerData))]
    [Union(1, typeof(HeadsetData))]
    public class SensorData
    {
        [Key("Position")]
        public Vector3 Position { get; set; }
        [Key("Orientation")]
        public Quaternion Orientation { get; set; }
        [Key("Time")]
        public float Time { get; set; }
    }

    [MessagePackObject]
    public class ControllerData : SensorData
    {
        [Key("ControllerState")]
        public ControllerState State { get; set; }
    }

    [MessagePackObject]
    public class HeadsetData : SensorData
    {

    }
    [MessagePackObject]
    public class VRGestureSample
    {
        [Key("GestureID")]
        public int GestureID { get; set; }
        [Key("SampleID")]
        public int SampleID { get; set; }
        [Key("GestureName")]
        public string GestureName { get; set; }
        [Key("Tag")]
        public string Tag { get; set; }

        [Key("HeadsetData")]
        public List<HeadsetData> HeadsetData { get; set; } = new List<HeadsetData>();
        [Key("RightControllerData")]
        public List<ControllerData> RightControllerData { get; set; } = new List<ControllerData>();
        [Key("LeftControllerData")]
        public List<ControllerData> LeftControllerData { get; set; } = new List<ControllerData>();
    }
}
