using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using Zenject;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class VideoMapPresenter : MonoBehaviour, IDisposable
    {
        public int m_VideoID = 0;
        public Button m_VideoMapButton;
        public int m_TaskID = 3;
        private IDisposable m_Sub;

        public void Dispose()
        {
            m_Sub.Dispose();
        }

        void Awake()
        {
            m_Sub = m_VideoMapButton.OnClickAsObservable().Subscribe(_ =>
            {
                MessageBroker.Default.Publish(new ShowVideoAction(m_VideoID));
                MessageBroker.Default.Publish(new ShowVideoTaskAction(m_TaskID));
            });
        }

        public class Factory : PlaceholderFactory<VideoMapPresenter>
        { 

        }

    }
}