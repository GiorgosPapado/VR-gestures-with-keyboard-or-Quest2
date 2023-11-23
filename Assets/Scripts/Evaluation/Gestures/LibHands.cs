using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Assets.Scripts.Evaluation.Gestures
{
    public static class LibHands
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct HandData
        {
            public Int32 nHands;
            public IntPtr pLandmarks;          // float *pLandmarks nHandsX21X3 (x,y,z) coordinates of 21 landmarks
            public IntPtr pHandedness;         // int* pHandedness nHands ints 0=Left 1=Right hand
            public IntPtr pHandednessConfidence; // float* nHands floats with confidence of handedness
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct ImageFrame
        {
            public IntPtr data;                // uint8_t* pdata
            public Int32 frame_width;        
            public Int32 frame_height;
        }

        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr CreateContext();
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern void FreeContext(IntPtr hInstance);

        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern void StartWebcam(IntPtr hInstance, int device_id, int resolution_width, int resolution_height, int fps);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern void StopWebcam(IntPtr hInstance);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern void SetWebcamMirrorFlip(IntPtr hInstance, [MarshalAs(UnmanagedType.I1)] bool value);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool GetWebcamMirrorFlip(IntPtr hInstance);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool GrabWebcamFrame(IntPtr hInstance);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern void GetLastWebcamFrame(IntPtr hInstance, out IntPtr out_frame);

        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool InitHandTracker(IntPtr hInstance, string pathToGraphProto);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool HandTrackerProcess(IntPtr hInstance, IntPtr input_frame);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool HandTrackerGetLastAnnotatedFrame(IntPtr hInstance, out IntPtr output_frame);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool HandTrackerGetLastFrameHands(IntPtr hInstance, out IntPtr out_hand_data);

        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern void InitMedianFilter(IntPtr hInstance, uint window_size);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool MedianFilterProcess(IntPtr hInstance, IntPtr hand_data);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool MedianFilterGetFilteredHands(IntPtr hInstance, out IntPtr out_hand_data);

        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern void InitAverageFilter(IntPtr hInstance, uint window_size);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool AverageFilterProcess(IntPtr hInstance, IntPtr hand_data);
        [DllImport("lib-hands.dll", CharSet = CharSet.Ansi)]
        public static extern bool AverageFilterGetFilteredHands(IntPtr hInstance, out IntPtr out_hand_data);
    }
}
