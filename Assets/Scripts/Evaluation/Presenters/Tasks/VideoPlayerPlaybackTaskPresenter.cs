using System.Collections;
using UnityEngine;
using System;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Model.Tasks;


namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class VideoPlayerPlaybackTaskPresenter : MonoBehaviour
    {

        public int ID;
        public string m_VideoPlaybackTaskTag;
        private IDisposable m_Sub;
        // Use this for initialization
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<VideoPlaybackTaskAction>().Where(x => x.Tag == m_VideoPlaybackTaskTag).Subscribe(_ =>
            {
                TaskComplete.Notify(ID, TaskType.VIDEO_PLAYER_PLAYBACK_COMPLETE);
            });
        }

        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}