using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using UniRx;

namespace Assets.Scripts.View
{    
    public class MsgView : MonoBehaviour
    {
        private Queue<PopUpMessage> m_Queue = new Queue<PopUpMessage>();        
        private PopUpMessage m_LastPopUpMessage;
        private float m_LastPopUpMessageStartShowTime;
        
        public Text m_Text;
        public Image m_Background;
        public Color m_NewMessageColor;
        private Color m_BackgroundColor;
        private void Start()
        {
            m_BackgroundColor = m_Background.color;
        }
        void Update()
        {

            if (m_LastPopUpMessage == null)
            {
                if (m_Queue.Count > 0)
                {
                    m_LastPopUpMessage = m_Queue.Dequeue();
                    m_LastPopUpMessageStartShowTime = Time.time;
                    SetText(m_LastPopUpMessage.Message);
                    SetColor();
                    Observable.Timer(TimeSpan.FromSeconds(m_LastPopUpMessage.MinDuration)).Subscribe(_ =>
                    {
                        if (!m_LastPopUpMessage.KeepColorAfterDuration)
                        {
                            m_Background.color = m_BackgroundColor;     // restore color
                        }
                        m_LastPopUpMessage = null;
                    });
                    
                }
            }
        }
        
        void SetText(string txt)
        {
            m_Text.text = txt;
        }
        void SetColor()
        {
            m_Background.color = m_NewMessageColor;
        }

        public void ShowMessage(PopUpMessage msg)
        {
            m_Queue.Enqueue(msg);
        }
    }
}
