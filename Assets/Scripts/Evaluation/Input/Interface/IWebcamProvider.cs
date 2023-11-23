using Assets.Scripts.Evaluation.Input.Webcam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input.Interface
{
    public interface IWebcamProviderCore
    {
        public WebcamConfig WebcamConfig { get; }
        public bool Enabled { get; set; }
        public Texture Texture { get; }
        public bool FlipUpsideDown { get; }
    }
}
