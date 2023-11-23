using System;
using System.Collections;
using UnityEngine;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Model.Tasks;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class VideoPlayerOpenedTaskPresenter : MonoBehaviour
    {
        public int ID;
        public string m_VideoPlayerPlaybackTaskTag;
        private IDisposable m_Sub;
        // Use this for initialization
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<ShowVideoTaskAction>().Where(x => x.Tag == m_VideoPlayerPlaybackTaskTag).Subscribe(_ => 
            {
                TaskComplete.Notify(ID, TaskType.VIDEO_PLAYER_WIDGET_CLICKED);
            });
        }

        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}