using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;
using Assets.Scripts.Evaluation.Gestures.Interface;
using Assets.Scripts.Evaluation.Utils;
using Assets.Scripts.Utils;
using System.IO;
using Assets.Scripts.Evaluation.Input.Devices;

namespace Assets.Scripts.Evaluation.Input.Replay
{
    public class Recorder : Activatable, ITickable, IRecorder
    {        
        private FileManager m_FileManager = new FileManager();
        private FileNameGenerator m_FileNameGenerator = new FileNameGenerator();
        private IStateCapturer m_StateCapturer;

        private int m_CurrentSessionID = 0;
        private int m_CurrentPartID = 0;
        private string m_CurrentDirName = string.Empty;        
        private string m_SessionType = "";
        public Recorder(IStateCapturer stateCapturer, DisplayDeviceType deviceType)
        {
            m_StateCapturer = stateCapturer;
            m_SessionType = deviceType == DisplayDeviceType.Monitor ? "webcam" : "vr";
            Observable.OnceApplicationQuit().Subscribe(_ =>
            {
                Enabled = false;        // flush recording
            });
        }

        private string GenerateFileName()
        {
            m_FileNameGenerator.FileNamePrefix = $"{m_CurrentDirName}\\part-{m_CurrentPartID.ToString("D8")}";
            m_FileNameGenerator.FileNameSuffix = ".bin";
            string CurrentPartFileName = m_FileNameGenerator.Generate();
            m_CurrentPartID++;
            return CurrentPartFileName;
        }

        protected override void OnEnable()
        {
            m_CurrentSessionID++;
            m_CurrentPartID = 0;
            m_CurrentDirName = m_FileManager.CreateDirectory($".\\{m_SessionType} - session-{m_CurrentSessionID}");            
        }

        protected override void OnDisable()
        {
            m_StateCapturer.FlushState(GenerateFileName(),false);       
        }                
        public void Tick()
        {            
            if(Enabled)
            {
                m_StateCapturer.CaptureState();   
            }
            if(m_StateCapturer.BufferSize > BufferSize)
            {
                m_StateCapturer.FlushState(GenerateFileName(),true);
            }
        }

        public int BufferSize { get; set; } = 60 * 30;
    }
}
