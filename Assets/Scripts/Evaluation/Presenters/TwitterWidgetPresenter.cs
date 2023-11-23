using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Assets.Scripts.Utils;
using Assets.Scripts.Evaluation.Model.Twitter;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using TMPro;
using Assets.Scripts.Evaluation.Input.Interface;
using Assets.Scripts.Evaluation.Utils;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class TwitterWidgetPresenter : MonoBehaviour, IInitializable
    {
        public GameObject m_TwitterWidget;
        public TextMeshProUGUI m_TwitterBodyText;
        public Button m_WidgetCloseButton;

        private string m_TwitterBodyTextTemplate;

        private Repository<TweetInfo> m_TweetRepository;
        private CompositeDisposable m_Sub = new CompositeDisposable();
        private TopWidgetTracker m_TopWidgetTracker;
        [Inject]
        public void Init(Repository<TweetInfo> repository, TopWidgetTracker topWidgetTracker)
        {
            m_TweetRepository = repository;
            m_TwitterBodyTextTemplate = m_TwitterBodyText.text;
            m_TopWidgetTracker = topWidgetTracker;
        }

        public void Initialize()
        {
            m_Sub.Add(MessageBroker.Default.Receive<ShowTweetInfoAction>().Subscribe(x =>
            {
                var info = m_TweetRepository[x.ID];
                string str = m_TwitterBodyTextTemplate.Replace("$user$", info.Username)
                .Replace("$date$", info.Date.ToString())
                .Replace("$ip$", info.IP)
                .Replace("$tweet$", info.Tweet)
                .Replace("$hashtags$", string.Join(" ", info.Hashtags));
                m_TwitterBodyText.text = str;
                m_TopWidgetTracker.PushWidget(m_TwitterWidget);
            }));

            m_Sub.Add(m_WidgetCloseButton.OnClickAsObservable().
                Merge(MessageBroker.Default.Receive<OKAction>().Select(_ => Unit.Default)).Merge(
                MessageBroker.Default.Receive<CancelAction>().Select(_=>Unit.Default))
                .Where(_ => m_TwitterWidget.activeInHierarchy && m_TopWidgetTracker.IsTop(m_TwitterWidget))
                .Subscribe(_ => {
                    m_TopWidgetTracker.PopWidget(m_TwitterWidget);
            }));            
        }
    }

}
