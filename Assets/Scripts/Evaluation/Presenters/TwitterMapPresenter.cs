using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Evaluation.Actions;
using UniRx;
using Zenject;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class TwitterMapPresenter : MonoBehaviour, IDisposable
    {
        public Button m_TwitterInfoButton;
        public int m_TweetID = 0;
        private IDisposable m_Sub;
        void Awake()
        {
            m_Sub = m_TwitterInfoButton.OnClickAsObservable().Subscribe(_ =>
            {
                MessageBroker.Default.Publish(new ShowTweetInfoAction(m_TweetID));
            });
        }
        public void Dispose()
        {
            m_Sub.Dispose();
        }

        public class Factory : PlaceholderFactory<TwitterMapPresenter>
        {

        }
    }
}
