using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Utils;
using Zenject;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class VideoPlayerPresenter : MonoBehaviour, IDisposable
    {
        public Button m_PlayButton;
        public Button m_StopButton;
        public Button m_CloseButton;
        public Slider m_FrameSlider;
        public Sprite m_PlayImg;
        public Sprite m_PauseImg;
        public VideoPlayer m_VideoPlayer;
        public VideoClip[] m_VideoClipRepository;
        private bool m_Playing = false;

        public GameObject m_VideoPlayerObject;
        public GameObject m_InitImg;
        public GameObject m_VideoTexture;
        private CompositeDisposable m_Subs = new CompositeDisposable();

        private bool m_PlayButtonClicked = false;
        private bool m_StopButtonClicked = false;
        private TopWidgetTracker m_TopWidgetTracker;

        [Inject]
        public void TopWidgetTracker(TopWidgetTracker topWidgetTracker)
        {
            m_TopWidgetTracker = topWidgetTracker;
        }
        private void StopVideo(bool force = false)
        {
            if (m_Playing || force)
            {
                Image button_img = m_PlayButton.GetComponent<Image>();
                button_img.sprite = m_PlayImg;
                m_Playing = false;
                m_VideoPlayer.Stop();
                m_FrameSlider.value = 0;
                m_InitImg.SetActive(true);
                m_VideoTexture.SetActive(false);
            }
        }
        private void StartPauseVideo()
        {
            Image button_img = m_PlayButton.GetComponent<Image>();
            if (!m_Playing)
            {
                // If the play button clicked and the player 
                // was not in a playback state, then we wish to start playback.
                // So we change the button's image to the "pause" image.
                m_VideoTexture.SetActive(true);
                m_Playing = true; 
                m_InitImg.SetActive(false);
                button_img.sprite = m_PauseImg;
                m_VideoPlayer.Play();
            }
            else
            {
                // If there is a video playing, then we wish to pause playback.'
                // so we change the image back to the original one.
                m_Playing = false;
                button_img.sprite = m_PlayImg;
                m_VideoPlayer.Pause();
            }
        }
        public void Awake()
        {
            m_VideoPlayer.playOnAwake = false;
            m_VideoTexture.SetActive(false);

            /// UI Subscriptions.
            m_Subs.Add(m_PlayButton.OnClickAsObservable().Merge(MessageBroker.Default.Receive<OKAction>()
                .Select(_ => Unit.Default)).Where(_=> m_VideoPlayerObject.activeInHierarchy).Subscribe(_ =>
            {
                StartPauseVideo();
                m_PlayButtonClicked = true;
            }));

            m_Subs.Add(m_StopButton.OnClickAsObservable().Subscribe(_ =>
            {
                StopVideo();
                m_StopButtonClicked = true;
            }));

            m_Subs.Add(MessageBroker.Default.Receive<ShowVideoAction>().Subscribe(video =>
            {
                m_VideoPlayer.clip = m_VideoClipRepository[video.ID];
                m_TopWidgetTracker.PushWidget(m_VideoPlayerObject);
            }));

            m_Subs.Add(m_CloseButton.OnClickAsObservable().Merge(MessageBroker.Default.Receive<CancelAction>().Select(_=>Unit.Default))
                .Where(_ => m_TopWidgetTracker.IsTop(m_VideoPlayerObject))
                .Subscribe(_ =>
            {
                StopVideo(true);
                m_TopWidgetTracker.PopWidget(m_VideoPlayerObject);
                if (m_PlayButtonClicked && m_StopButtonClicked)
                {
                    MessageBroker.Default.Publish(new VideoPlaybackTaskAction("VideoPlaybackTaskTag"));
                }
            }));
        }

        public void Update()
        {
            if (m_VideoPlayer.frame > 0)
            {
                m_FrameSlider.value = (float)m_VideoPlayer.frame / (float)m_VideoPlayer.frameCount;
            }
            else
            {
                m_FrameSlider.value = 0;
                //m_VideoPlayer.clip = m_VideoClipRepository[2];
            }
        }

        public void Dispose()
        {
            m_Subs.Dispose();
        }
    }

}