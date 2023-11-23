//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Video;
//using UniRx;

//[RequireComponent(typeof(VideoPlayer))]
//public class VideoPlayerStateManager : MonoBehaviour
//{
//    VideoPlayer m_VideoPlayer;
//    private bool m_FirstFrame = true;
//    private void Awake()
//    {
//        m_VideoPlayer = GetComponent<VideoPlayer>();        
//    }

//    private void VideoPlayer_seekCompleted(VideoPlayer source)
//    {
//        //source.Pause();
//        //m_VideoPlayer.seekCompleted -= VideoPlayer_seekCompleted;
//    }

//    private void OnEnable()
//    {
//        m_VideoPlayer.seekCompleted += VideoPlayer_seekCompleted;
//        m_VideoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
//        m_VideoPlayer.Prepare();        
//    }

//    private void VideoPlayer_prepareCompleted(VideoPlayer source)
//    {
//        m_VideoPlayer.prepareCompleted -= VideoPlayer_prepareCompleted;
//        m_VideoPlayer.frame = 0;
//        m_VideoPlayer.sendFrameReadyEvents = true;
//        m_VideoPlayer.frameReady += VideoPlayer_frameReady;
//        m_VideoPlayer.Play();
        
//    }

//    private void VideoPlayer_frameReady(VideoPlayer source, long frameIdx)
//    {
//        m_VideoPlayer.frameReady -= VideoPlayer_frameReady;
//        m_VideoPlayer.Pause();
//    }
//}
