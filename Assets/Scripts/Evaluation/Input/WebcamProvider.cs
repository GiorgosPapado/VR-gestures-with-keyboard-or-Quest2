using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using Zenject;
using Assets.Scripts.Evaluation.Utils;
using Assets.Scripts.Evaluation.Input.Interface;

namespace Assets.Scripts.Evaluation.Input
{
    public class WebcamProvider : Activatable, ITickable, IDisposable
    {             
        private Subject<Unit> m_OnWebcamStreamEnabledSubject = new Subject<Unit>();
        private Subject<Unit> m_OnWebcamStreamDisabledSubject = new Subject<Unit>();
        private IWebcamProviderCore m_WebcamProviderCore;

        public KeyCode m_ActivationKey = KeyCode.H;

        public WebcamProvider(IWebcamProviderCore core)
        {
            m_WebcamProviderCore = core;
        }

        protected override void OnEnable()
        {
            m_WebcamProviderCore.Enabled = true;
            m_OnWebcamStreamEnabledSubject.OnNext(Unit.Default);
        }

        protected override void OnDisable()
        {
            m_WebcamProviderCore.Enabled = false;
            m_OnWebcamStreamDisabledSubject.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            Enabled = false;
        }

        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(m_ActivationKey))
                Enabled = !Enabled;
        }

        public IWebcamProviderCore Core
        {
            get
            {
                return m_WebcamProviderCore;
            }
        }

        public IObservable<Unit> OnWebcamStreamEnabled
        {
            get
            {
                return m_OnWebcamStreamEnabledSubject.AsObservable();
            }
        }

        public IObservable<Unit> OnWebcamStreamDisabled
        {
            get
            {
                return m_OnWebcamStreamDisabledSubject.AsObservable();
            }
        }
    }
}
