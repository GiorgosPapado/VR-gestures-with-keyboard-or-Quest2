using System;
using Assets.Scripts.Evaluation.Model;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Gestures.ONNX;
using Assets.Scripts.Evaluation.Gestures.Interface;

namespace Assets.Scripts.Evaluation.Input.VRController
{
    public class VRGestureProvider : IInitializable, IVRControllerGestureProvider
    {
        private CompositeDisposable m_Subs = new CompositeDisposable();
        private VRGestureSample m_CurrentSample;        
        private SensorTracker m_InputTracker;
        private Subject<VRGestureSample> m_OnGestureRecordedSubject = new Subject<VRGestureSample>();
        VRGestureRecordTrigger m_GestureTrigger;
        private VRControllerGestures m_VRControllerGestures;
        private bool m_IsStarted = false;
        private BehaviorSubject<VRControllerGestureType> m_VRControllerGestureStreamSubject = new BehaviorSubject<VRControllerGestureType>(VRControllerGestureType.None);
        private Subject<SensorDataBatch> m_LiveGestureDataStream = new Subject<SensorDataBatch>();
        public VRGestureProvider(SensorTracker inputTracker, VRGestureRecordTrigger gestureTrigger, VRControllerGestures controllerGestures)
        {
            m_InputTracker = inputTracker;
            m_GestureTrigger = gestureTrigger;
            m_VRControllerGestures = controllerGestures;
        }
        private void Start()
        {
            m_CurrentSample = new VRGestureSample();
            //m_InputTracker.enabled = true;
            m_IsStarted = true;
        }
        private void Stop()
        {
            if (m_IsStarted)
            {
                //m_InputTracker.enabled = false;
                m_VRControllerGestureStreamSubject.OnNext(m_VRControllerGestures.Inference(m_CurrentSample));
                m_OnGestureRecordedSubject.OnNext(m_CurrentSample);                
            }
            m_IsStarted = false;
        }
        public void Initialize()
        {
            m_Subs.Add(m_GestureTrigger.OnGestureRecordStart.Subscribe(_ => Start()));
            m_Subs.Add(m_GestureTrigger.OnGestureRecordStop.Subscribe(_ => Stop()));
            m_Subs.Add(m_InputTracker.OnSensorDataAvailable.Where(_ => m_IsStarted).Subscribe(x =>
            {
                if(x!=null)
                {
                    m_CurrentSample.LeftControllerData.Add(x.LeftController);
                    m_CurrentSample.RightControllerData.Add(x.RightController);
                    m_CurrentSample.HeadsetData.Add(x.Headset);
                    m_LiveGestureDataStream.OnNext(x);
                }
            }));
            m_Subs.Add(Observable.EveryEndOfFrame().Subscribe(_ =>
            {
                ResetLatestControllerGesture();
            }));
        }

        public IObservable<VRGestureSample> OnGestureRecorded
        {
            get
            {
                return m_OnGestureRecordedSubject.AsObservable();
            }
        }

        public IObservable<VRControllerGestureType> VRControllerGestureStream => m_VRControllerGestureStreamSubject.AsObservable();

        public VRControllerGestureType GetLatestVRControllerGesture()
        {
            return m_VRControllerGestureStreamSubject.Value;
        }

        public void ResetLatestControllerGesture()
        {
            m_VRControllerGestureStreamSubject.OnNext(VRControllerGestureType.None);
        }

        public IObservable<SensorDataBatch> LiveGestureDataStream => m_LiveGestureDataStream.AsObservable();
    }
}
