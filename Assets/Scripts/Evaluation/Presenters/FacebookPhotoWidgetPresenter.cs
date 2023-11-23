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
using Assets.Scripts.Evaluation.Model.Facebook;
using Assets.Scripts.Evaluation.Utils;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class FacebookPhotoWidgetPresenter : MonoBehaviour, IInitializable
    {
        public GameObject m_FbPhotoWidget;
        public TextMeshProUGUI m_FacebookBodyText;
        public Image m_FacebookBodyPhoto;
        public Button m_WidgetCloseButton;
        public Sprite[] m_PhotoRepository;
        public string m_TaskTag;
        private string m_FbBodyTextTemplate;

        private Repository<FbInfo> m_FbRepository;
        private CompositeDisposable m_Sub = new CompositeDisposable();
        private TopWidgetTracker m_TopWidgetTracker;
        [Inject]
        public void Init(Repository<FbInfo> repository, TopWidgetTracker topWidgetTracker)
        {
            m_FbRepository = repository;
            m_FbBodyTextTemplate = m_FacebookBodyText.text;
            m_TopWidgetTracker = topWidgetTracker;
        }

        public void Initialize()
        {
            m_Sub.Add(MessageBroker.Default.Receive<ShowFacebookInfoAction>().Subscribe(x =>
            {
                var info = m_FbRepository[x.ID];
                string str = m_FbBodyTextTemplate.Replace("$user$", info.Username)
                .Replace("$date$", info.Date.ToString())
                .Replace("$ip$", info.IP)
                .Replace("$post$", info.Post)
                .Replace("$hashtags$", string.Join(" ", info.Hashtags));
                m_FacebookBodyText.text = str;
                m_FacebookBodyPhoto.sprite = m_PhotoRepository[x.ID];
                m_TopWidgetTracker.PushWidget(m_FbPhotoWidget);
            }));

            m_Sub.Add(m_WidgetCloseButton.OnClickAsObservable().Merge(MessageBroker.Default.Receive<OKAction>().Select(_ => Unit.Default).Merge(
               MessageBroker.Default.Receive<CancelAction>().Select(_ => Unit.Default)).Where(_ => m_FbPhotoWidget.activeInHierarchy && m_TopWidgetTracker.IsTop(m_FbPhotoWidget))).Subscribe(_ => {

                m_TopWidgetTracker.PopWidget(m_FbPhotoWidget);
                MessageBroker.Default.Publish(new FacebookWidgetClickAction(m_TaskTag));
            }));
        }
    }

}
