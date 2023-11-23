using System;
using UnityEngine;
using UniRx;

namespace Assets.Scripts.Evaluation.Input.VRController
{
    public class SensorTracker : MonoBehaviour
    {
        public Transform HMD;
        public Transform RightController;
        public Transform LeftController;

        private BehaviorSubject<SensorDataBatch> m_OnSensorDataAvailableSubject = new BehaviorSubject<SensorDataBatch>(null);
        
        public void Update()
        {
            SensorDataBatch data = new SensorDataBatch();
            data.Headset.Position = HMD.position;
            data.Headset.Orientation = HMD.rotation;
            data.RightController.Position = RightController.position;
            data.RightController.Orientation = RightController.rotation;
            data.LeftController.Position = LeftController.position;
            data.LeftController.Orientation = LeftController.rotation;
            float timestamp = Time.time;
            data.LeftController.Time = timestamp;
            data.RightController.Time = timestamp;
            data.Headset.Time = timestamp;
            m_OnSensorDataAvailableSubject.OnNext(data);
        }

        public IObservable<SensorDataBatch> OnSensorDataAvailable
        {
            get
            {
                return m_OnSensorDataAvailableSubject.AsObservable();
            }
        }

        public SensorDataBatch GetLatestSensorData()
        {
            return m_OnSensorDataAvailableSubject.Value;
        }
    }
}
