using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using Assets.Scripts.Evaluation.Actions;
using TMPro;
using Assets.Scripts.Evaluation.Utils;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class HUDPresenter : MonoBehaviour, IInitializable, IDisposable
    {
        public Button m_CloseButton;
        public GameObject m_HUD;
        public TextMeshProUGUI m_Text;
        private CompositeDisposable m_Subs = new CompositeDisposable();
        private Queue<ShowInfoOnHUDAction> m_Infos = new Queue<ShowInfoOnHUDAction>();
        private bool m_Showing = false;
        private IDisposable m_TimerSub = null;
        private TopWidgetTracker m_TopWidgetTracker;
        public void Dispose()
        {
            m_Subs.Dispose();
        }

        [Inject]
        public void Init(TopWidgetTracker topWidgetTracker)
        {
            m_TopWidgetTracker = topWidgetTracker;
        }

        public void Initialize()
        {
            m_Subs.Add(m_CloseButton.OnClickAsObservable().Merge(MessageBroker.Default.Receive<CancelAction>().Select(_=>Unit.Default))
               .Where(_=> m_HUD.activeInHierarchy && m_TopWidgetTracker.IsTop(m_HUD))
               .Subscribe(_ =>
            {
                if(m_TimerSub!=null)
                    m_TimerSub.Dispose();

                if (m_Infos.Count > 0)
                    m_Infos.Dequeue();                
                m_TopWidgetTracker.PopWidget(m_HUD);
                m_Showing = false;
            }));

            m_Subs.Add(MessageBroker.Default.Receive<ShowInfoOnHUDAction>().Subscribe(info =>
            {                
                m_Infos.Enqueue(info);                
            }
            ));
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {             
            if(!m_Showing && m_Infos.Count>0)
            {
                var info = m_Infos.Peek();
                if (info.Duration != 0.0)
                {
                    m_TimerSub = Observable.Timer(TimeSpan.FromSeconds(info.Duration)).Subscribe(_ =>
                    {
                        if (m_Infos.Count > 0)
                        {
                            m_Infos.Dequeue();
                        }
                        m_TopWidgetTracker.PopWidget(m_HUD);
                        m_Showing = false;
                    });
                }
                m_Text.text = info.InfoText;
                m_TopWidgetTracker.PushWidget(m_HUD);
                m_Showing = true;
            }
        }
    }
}

