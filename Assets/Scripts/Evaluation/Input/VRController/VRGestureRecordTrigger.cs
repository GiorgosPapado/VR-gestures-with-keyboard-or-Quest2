using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input.VRController
{
    public class VRGestureRecordTrigger : MonoBehaviour
    {        
        private Subject<EventArgs> m_OnGestureRecordStart = new Subject<EventArgs>();
        private Subject<EventArgs> m_OnGestureRecordStop = new Subject<EventArgs>();

        /*
        public void OnRecordGestureActionChange(SteamVR_Behaviour_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
        {
            if (newState)
                m_OnGestureRecordStart.OnNext(EventArgs.Empty);
            else
                m_OnGestureRecordStop.OnNext(EventArgs.Empty);
        }*/

        public IObservable<EventArgs> OnGestureRecordStart
        {
            get
            {
                return m_OnGestureRecordStart.AsObservable();
            }
        }

        public IObservable<EventArgs> OnGestureRecordStop
        {
            get
            {
                return m_OnGestureRecordStop.AsObservable();
            }
        }
    }
}
