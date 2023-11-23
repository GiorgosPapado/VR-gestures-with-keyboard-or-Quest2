using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Evaluation.Input.Interface;
using UnityEngine;
using Assets.Scripts.Evaluation.Utils;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Gestures;
using System.Runtime.InteropServices;
namespace Assets.Scripts.Evaluation.Input.Webcam
{
    public class LibHandsWebcamProviderCore : Activatable, IWebcamProviderCore
    {
        private KeyCode m_RawWebcamStreamHotKey = KeyCode.R;
        private KeyCode m_AnnotatedHandsWebcamStreamHotKey = KeyCode.T;
        private LibHandsCore m_LibHandsCore;

        public LibHandsWebcamProviderCore(LibHandsCore libHandsCore)
        {
            m_LibHandsCore = libHandsCore;
        }

        protected override void OnEnable()
        {
            m_LibHandsCore.Enabled = true;
            m_LibHandsCore.EnableAnnotatedHandTrackingWebcamStream = true;
            m_LibHandsCore.EnableHandLandmarkStream = true;
            m_LibHandsCore.EnableGestureRecognitionStream = true;
            m_LibHandsCore.EnableMajorityVotingGestureRecognititonFilter = true;
        }
        protected override void OnDisable()
        {
            m_LibHandsCore.Enabled = false;
        }
        public WebcamConfig WebcamConfig { get; } = new WebcamConfig();
        public bool FlipUpsideDown => true;

        public Texture Texture => m_LibHandsCore.AnnotatedHandTrackingWebcamTexture;
    }
}
