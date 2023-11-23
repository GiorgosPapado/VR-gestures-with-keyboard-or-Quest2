using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using Zenject;
using Assets.Scripts.Evaluation.Utils;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class HelpWidgetCloser : MonoBehaviour
    {
        public Button[] m_CloseButtons;
        public GameObject[] m_Widgets;
        private TopWidgetTracker m_TopWidgetTracker;

        private CompositeDisposable m_Sub = new CompositeDisposable();

        [Inject]
        public void Init(TopWidgetTracker topWidgetTracker)
        {
            m_TopWidgetTracker = topWidgetTracker;
        }
        public void Start()
        {
            for(int i=0;i<m_CloseButtons.Length;i++)
            {
                m_Sub.Add(m_CloseButtons[i].OnClickAsObservable().Merge(MessageBroker.Default.Receive<CancelAction>().Select(_ => Unit.Default)).WithLatestFrom(Observable.Return(i),(l,r) => r).
                 Where(index => m_Widgets[index].activeInHierarchy && m_TopWidgetTracker.IsTop(m_Widgets[index]))
                .Subscribe(idx =>
                {
                    m_TopWidgetTracker.PopWidget(m_Widgets[idx]);
                }));               
            }
        }
    }
}
