using Assets.Scripts.Evaluation.Model;
using MessagePack;
namespace Assets.Scripts.Evaluation.Input.VRController
{
    [MessagePackObject]
    public class SensorDataBatch
    {
        [Key("LeftController")]
        public ControllerData LeftController { get; set; }
        [Key("RightController")]
        public ControllerData RightController { get; set; }
        [Key("Headset")]
        public HeadsetData Headset { get; set; }
        public SensorDataBatch()
        {
            LeftController = new ControllerData();
            RightController = new ControllerData();
            Headset = new HeadsetData();
        }
        public SensorDataBatch(ControllerData leftController, ControllerData rightController, HeadsetData headset)
        {
            LeftController = leftController;
            RightController = rightController;
            Headset = headset;
        }
    }
}
