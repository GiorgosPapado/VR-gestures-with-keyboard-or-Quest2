using Assets.Scripts.Evaluation.Input.Replay;
using Assets.Scripts.Evaluation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Input;

namespace Assets.Scripts.Evaluation.Views
{
    public class ReplayIndicator : MonoBehaviour
    {
        public Color m_RecordingColor;
        public Color m_PlaybackColor;
        public Color m_NeutralColor;
        public TextMeshProUGUI m_IndicatorText;
        public Image m_Indicator;
        public GameObject m_IndicatorObject;

        private IRecorder m_Recorder;
        private IPlayer m_Player;
        private InputInterfaceName m_LastInterfaceName;
        private InputInterfaceSelector m_InterfaceSelector;

        private void OnPlaybackFinished()
        {
            m_Indicator.color = m_NeutralColor;
            m_IndicatorText.text = "Idle";
            m_InterfaceSelector.SetActiveInputInterface(m_LastInterfaceName);
        }

        [Inject]
        public void Init(IRecorder recorder, IPlayer player, InputInterfaceSelector interfaceSelector)
        {
            m_Recorder = recorder;
            m_Player = player;
            m_InterfaceSelector = interfaceSelector;
            m_Player.OnPlaybackFinished.Subscribe( _ =>
            {
                OnPlaybackFinished();
            });
        }

        public void Update()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                if(!m_Player.Enabled)
                {
                    m_Recorder.Enabled = !m_Recorder.Enabled;
                    if (m_Recorder.Enabled)
                    {                        
                        m_Indicator.color = m_RecordingColor;
                        m_IndicatorText.text = "Recording";
                    }
                    else
                    {
                        m_Indicator.color = m_NeutralColor;
                        m_IndicatorText.text = "Idle";
                    }
                }
            }
            else if(UnityEngine.Input.GetKeyDown(KeyCode.P))
            {
                if(!m_Recorder.Enabled)
                {
                    m_Player.Enabled = !m_Player.Enabled;
                    if (m_Player.Enabled)
                    {
                        m_LastInterfaceName = m_InterfaceSelector.ActiveInputInterfaceName;
                        m_InterfaceSelector.SetActiveInputInterface(InputInterfaceName.ReplayWebcam);
                        m_Indicator.color = m_PlaybackColor;
                        m_IndicatorText.text = "Playback";
                    }
                    else
                    {
                        OnPlaybackFinished();
                    }
                }
            }
        }
    }
}
