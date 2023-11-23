using Assets.Scripts.Evaluation.Gestures.Interface;
using Assets.Scripts.Evaluation.Input.Webcam;
using Assets.Scripts.Evaluation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;
using Assets.Scripts.Evaluation.Gestures.ONNX;
using Assets.Scripts.Evaluation.Gestures.Filters;

namespace Assets.Scripts.Evaluation.Gestures
{
    public enum HandSkeletonFilterType
    {
        None,
        Median,
        Average
    }
    public class LibHandsCore : Activatable, IHandLandmarkProvider, IHandGestureProvider, IInitializable, IDisposable
    {
        private IDisposable m_Sub = null;
        private IntPtr m_hInstance;
        private Texture2D m_RawWebcamTexture;
        private Texture2D m_AnnotatedHandTrackingWebcamTexture;
        private HandGestures m_HandGestures;
        private MajorityVotingFilter<HandGestureType> m_MajorityVotingFilter = new MajorityVotingFilter<HandGestureType>();

        private bool m_EnableRawWebcamStream;
        private bool m_EnableAnnotatedHandTrackingWebcamStream;
        private bool m_EnableHandLandmarkStream;
        private bool m_EnableGestureRecognitionStream;        

        private const string m_PathToMediapipeHandTrackingProtoFile = "./mediapipe/graphs/hand_tracking/hand_tracking_desktop_live_lmarks.pbtxt";

        private BehaviorSubject<HandData> m_HandLandmarkStreamSubject = new BehaviorSubject<HandData>(null);
        private BehaviorSubject<HandGestureType> m_HandGestureStreamSubject = new BehaviorSubject<HandGestureType>(HandGestureType.None);

        public LibHandsCore(HandGestures handGestures)
        {
            m_HandGestures = handGestures;
            m_MajorityVotingFilter.DefaultValueIfThresholdNotExceeded = HandGestureType.None;
        }

        public void Initialize()
        {
            m_hInstance = LibHands.CreateContext();
            LibHands.InitHandTracker(m_hInstance, m_PathToMediapipeHandTrackingProtoFile);
            LibHands.InitMedianFilter(m_hInstance, 7);
            LibHands.InitAverageFilter(m_hInstance, 7);
        }
        private void CreateTexture(out Texture2D texture)
        {
            texture = new Texture2D(WebcamConfig.Width, WebcamConfig.Height, TextureFormat.RGB24, false);
        }
        private void UpdateTexture(LibHands.ImageFrame frame, ref Texture2D texture)
        {
            if (texture == null)
            {
                texture = new Texture2D(frame.frame_width, frame.frame_height, TextureFormat.RGB24, false);
            }
            Debug.Assert(frame.frame_width == texture.width && frame.frame_height == texture.height);

            texture.LoadRawTextureData(frame.data, 3 * frame.frame_width * frame.frame_height);
            texture.Apply();
        }

        private void StartLoopIfRequired()
        {
            if (Enabled && m_Sub == null)
            {
                bool initloop = EnableRawWebcamStream || EnableAnnotatedHandTrackingWebcamStream || EnableHandLandmarkStream || EnableGestureRecognitionStream;
                if (initloop)
                {
                    CreateTexture(out m_RawWebcamTexture);
                    CreateTexture(out m_AnnotatedHandTrackingWebcamTexture);
                    LibHands.StartWebcam(m_hInstance, 0, WebcamConfig.Width, WebcamConfig.Height, WebcamConfig.FPS);
                    m_Sub = Observable.Interval(TimeSpan.FromSeconds(1.0 / WebcamConfig.FPS)).ObserveOnMainThread().Subscribe(_ => {

                        if (!LibHands.GrabWebcamFrame(m_hInstance))
                            return;

                        do
                        {
                            IntPtr pImageData = new IntPtr();
                            if (EnableRawWebcamStream || HandTrackingEnabled)
                            {
                                LibHands.GetLastWebcamFrame(m_hInstance, out pImageData);
                                if (EnableRawWebcamStream)
                                {
                                    LibHands.ImageFrame imgFrame;
                                    imgFrame = (LibHands.ImageFrame)Marshal.PtrToStructure(pImageData, typeof(LibHands.ImageFrame));
                                    UpdateTexture(imgFrame, ref m_RawWebcamTexture);
                                }
                            }

                            if (HandTrackingEnabled)
                            {
                                if (!LibHands.HandTrackerProcess(m_hInstance, pImageData))
                                    break;

                                if (EnableAnnotatedHandTrackingWebcamStream)
                                {
                                    IntPtr pAnnotatedImageData;
                                    LibHands.HandTrackerGetLastAnnotatedFrame(m_hInstance, out pAnnotatedImageData);
                                    LibHands.ImageFrame imgFrame;
                                    imgFrame = (LibHands.ImageFrame)Marshal.PtrToStructure(pAnnotatedImageData, typeof(LibHands.ImageFrame));
                                    UpdateTexture(imgFrame, ref m_AnnotatedHandTrackingWebcamTexture);
                                }

                                if (EnableHandLandmarkStream)
                                {
                                    IntPtr pHandData;
                                    if(LibHands.HandTrackerGetLastFrameHands(m_hInstance, out pHandData))
                                    {

                                        if (SkeletonFilter == HandSkeletonFilterType.Median)
                                        {
                                            LibHands.MedianFilterProcess(m_hInstance, pHandData);
                                            LibHands.MedianFilterGetFilteredHands(m_hInstance, out pHandData);
                                        }
                                        else if(SkeletonFilter == HandSkeletonFilterType.Average)
                                        {
                                            LibHands.AverageFilterProcess(m_hInstance, pHandData);
                                            LibHands.AverageFilterGetFilteredHands(m_hInstance, out pHandData);
                                        }

                                        LibHands.HandData handData;
                                        handData = (LibHands.HandData)Marshal.PtrToStructure(pHandData, typeof(LibHands.HandData));
                                        m_HandLandmarkStreamSubject.OnNext(new HandData(handData));
                                    }
                                    else { 
                                        m_HandLandmarkStreamSubject.OnNext(null);
                                    }

                                    if(EnableGestureRecognitionStream)
                                    {
                                        HandGestureType gesture = m_HandGestures.Inference(GetLatestHandLandmarks());
                                        if (EnableMajorityVotingGestureRecognititonFilter)
                                            gesture = m_MajorityVotingFilter.AddSample(gesture);
                                        //Debug.Log("Gesture type: " + gesture.ToString());
                                        m_HandGestureStreamSubject.OnNext(gesture);
                                    }
                                }
                            }
                        } while (false);                        
                    });
                }
            }
        }

        private void StopLoopIfRequired()
        {
            bool stoploop = !(EnableRawWebcamStream || EnableAnnotatedHandTrackingWebcamStream || EnableHandLandmarkStream || EnableGestureRecognitionStream);
            if (m_Sub != null && (!Enabled || stoploop))
            {
                m_Sub.Dispose();
                m_Sub = null;
                LibHands.StopWebcam(m_hInstance);
                // free texture memory ??
                m_RawWebcamTexture = null;
                m_AnnotatedHandTrackingWebcamTexture = null;
            }
        }

        protected override void OnEnable()
        {
            StartLoopIfRequired();
        }
        protected override void OnDisable()
        {
            StopLoopIfRequired();
        }
        public void Dispose()
        {
            Enabled = false;
            LibHands.FreeContext(m_hInstance);
        }

        public HandData GetLatestHandLandmarks()
        {
            return m_HandLandmarkStreamSubject.Value;
        }

        public HandGestureType GetLatestHandGesture()
        {
            return m_HandGestureStreamSubject.Value;
        }

        public Texture RawWebcamTexture => m_RawWebcamTexture;
        public Texture AnnotatedHandTrackingWebcamTexture => m_AnnotatedHandTrackingWebcamTexture;

        public bool EnableRawWebcamStream
        {
            get
            {
                return m_EnableRawWebcamStream;
            }
            set
            {
                if (value != m_EnableRawWebcamStream)
                {
                    m_EnableRawWebcamStream = value;
                    if (m_EnableRawWebcamStream)
                        StartLoopIfRequired();
                    else { 
                        StopLoopIfRequired();
                    }
                }
            }
        }
        public bool EnableAnnotatedHandTrackingWebcamStream
        {
            get
            {
                return m_EnableAnnotatedHandTrackingWebcamStream;
            }
            set
            {
                if (m_EnableAnnotatedHandTrackingWebcamStream != value)
                {
                    m_EnableAnnotatedHandTrackingWebcamStream = value;
                    if (m_EnableAnnotatedHandTrackingWebcamStream)
                        StartLoopIfRequired();
                    else
                        StopLoopIfRequired();
                }
            }
        }
        public bool EnableHandLandmarkStream
        {
            get
            {
                return m_EnableHandLandmarkStream;
            }
            set
            {
                if (m_EnableHandLandmarkStream != value)
                {
                    m_EnableHandLandmarkStream = value;
                    if (m_EnableHandLandmarkStream)
                        StartLoopIfRequired();
                    else
                        StopLoopIfRequired();
                }
            }
        }
        public bool EnableGestureRecognitionStream
        {
            get
            {
                return m_EnableGestureRecognitionStream;
            }
            set
            {
                if (m_EnableGestureRecognitionStream != value)
                {
                    m_EnableGestureRecognitionStream = value;
                    if (m_EnableGestureRecognitionStream)
                        StartLoopIfRequired();
                    else
                        StopLoopIfRequired();
                }
            }
        }

        public bool EnableMajorityVotingGestureRecognititonFilter { get; set; } = false;

        public HandSkeletonFilterType SkeletonFilter { get; set; } = HandSkeletonFilterType.Average;

        private bool HandTrackingEnabled
        {
            get
            {
                return EnableAnnotatedHandTrackingWebcamStream || EnableHandLandmarkStream || EnableGestureRecognitionStream;
            }
        }

        public WebcamConfig WebcamConfig { get; } = new WebcamConfig();

        public IObservable<HandData> HandLandmarkStream => m_HandLandmarkStreamSubject.AsObservable();

        public IObservable<HandGestureType> HandGestureStream => m_HandGestureStreamSubject.AsObservable();
    }
}
