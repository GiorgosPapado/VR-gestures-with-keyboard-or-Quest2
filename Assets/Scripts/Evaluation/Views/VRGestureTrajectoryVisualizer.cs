using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Assets.Scripts.Evaluation.Input.VRController;
using UniRx;
using System;

namespace Assets.Scripts.View
{
    public class VRGestureTrajectoryVisualizer : MonoBehaviour
    {        
        public GameObject m_TrackingPrefab;
        public float TrackedObjectsTTL = 0.3f;

        //private Subject<SensorDataBatch> m_SensorDataFlowInSubject = new Subject<SensorDataBatch>();
        //private SensorTracker m_SensorTracker;
        private VRGestureProvider m_VRGestureProvider;
        private CompositeDisposable m_Subs = new CompositeDisposable();

        private Queue<Tuple<GameObject, float>> m_RightControllerObj = new Queue<Tuple<GameObject, float>>();
        private Queue<Tuple<GameObject, float>> m_LeftControllerObj = new Queue<Tuple<GameObject, float>>();

        /*
        [Inject]
        public void Inject(SensorTracker sensorTracker)
        {
            m_SensorTracker = sensorTracker;
            m_Subs.Add(m_SensorTracker.OnSensorDataAvailable.Subscribe(x =>
            {
                if (x != null)
                {
                    var go = Instantiate(m_TrackingPrefab, x.RightController.Position, x.RightController.Orientation, transform);
                    m_RightControllerObj.Enqueue(Tuple.Create(go, Time.time));
                    go = Instantiate(m_TrackingPrefab, x.LeftController.Position, x.LeftController.Orientation, transform);
                    m_LeftControllerObj.Enqueue(Tuple.Create(go, Time.time));
                }
            }));
        }*/
        [Inject]
        public void Inject(VRGestureProvider vrGestureProvider)
        {
            m_VRGestureProvider = vrGestureProvider;
            m_Subs.Add(m_VRGestureProvider.LiveGestureDataStream.Subscribe(x =>
            {
                if (x != null)
                {
                    var go = Instantiate(m_TrackingPrefab, x.RightController.Position, x.RightController.Orientation, transform);
                    m_RightControllerObj.Enqueue(Tuple.Create(go, Time.time));
                    go = Instantiate(m_TrackingPrefab, x.LeftController.Position, x.LeftController.Orientation, transform);
                    m_LeftControllerObj.Enqueue(Tuple.Create(go, Time.time));
                }
            }));
        }
        void UpdateQueue(Queue<Tuple<GameObject, float>> queue)
        {
            while (queue.Count > 0 && (Time.time - queue.Peek().Item2) > TrackedObjectsTTL)
            {
                var go = queue.Dequeue();
                Destroy(go.Item1);
            }
        }
        // Update is called once per frame
        void Update()
        {
            UpdateQueue(m_LeftControllerObj);
            UpdateQueue(m_RightControllerObj);
        }
    }

}
