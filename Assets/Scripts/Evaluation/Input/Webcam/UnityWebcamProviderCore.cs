using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Evaluation.Input.Interface;
using UnityEngine;
using Assets.Scripts.Evaluation.Utils;

namespace Assets.Scripts.Evaluation.Input.Webcam
{
    public class UnityWebcamProviderCore : Activatable, IWebcamProviderCore
    {
        private WebCamTexture m_WebcamTexture;        

        protected override void OnEnable()
        {
            m_WebcamTexture = new WebCamTexture(WebcamConfig.Width, WebcamConfig.Height, WebcamConfig.FPS);
            m_WebcamTexture.Play();            
        }
        protected override void OnDisable()
        {
            m_WebcamTexture.Stop();
            m_WebcamTexture = null;            
        }
        public WebcamConfig WebcamConfig { get; } = new WebcamConfig();
        public bool FlipUpsideDown => false;
        public Texture Texture { get => m_WebcamTexture; } 
    }
}
