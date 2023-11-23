using System;

namespace Assets.Scripts.Evaluation.Input
{
    public enum InputInterfaceName
    {
        KeyboardMouse,
        WebcamGestures,
        VRControllers,
        ReplayWebcam
    }

    public static class InputInterfaceNameExtensions
    {        
        public static string ToReadableString(this InputInterfaceName inputInterfaceName)
        {
            switch(inputInterfaceName)
            {
                case InputInterfaceName.KeyboardMouse:
                    return "Keyboard - Mouse";
                case InputInterfaceName.WebcamGestures:
                    return "Webcam - Gestures";
                case InputInterfaceName.VRControllers:
                    return "VR Controllers";                
            }
            return string.Empty;
        }
    }
}