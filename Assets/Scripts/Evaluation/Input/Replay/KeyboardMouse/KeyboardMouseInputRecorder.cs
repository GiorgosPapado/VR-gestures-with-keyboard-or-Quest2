using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Evaluation.Input.Replay
{
    public class KeyboardMouseInputRecorder : MonoBehaviour
    {
        private KeyboardMouseState m_State = new KeyboardMouseState();        

        // update called before ongui
        private void Update()
        {
            m_State = new KeyboardMouseState();
            m_State.MousePosition = UnityEngine.Input.mousePosition;
            m_State.ScreenResolution = new Vector2(Screen.width, Screen.height);          
        }

        private void OnGUI()
        {
            var e = Event.current;
            if(e.isMouse)
            {                
                switch (e.type)
                {
                    case EventType.MouseDown:
                        m_State.MouseButonDownEvent.Add(e.button);
                        break;
                    case EventType.MouseUp:
                        m_State.MouseButtonUpEvent.Add(e.button);
                        break;                    
                }
            }
            else if(e.isKey)
            {
                switch(e.type)
                {
                    case EventType.KeyDown:
                        m_State.KeyModifiers = e.modifiers;
                        m_State.KeyDownEvent.Add(e.keyCode);
                        break;
                    case EventType.KeyUp:
                        m_State.KeyUpEvent.Add(e.keyCode);
                        break;                                                
                }
            }
        }

        public KeyboardMouseState GetState()
        {
            return m_State;
        }
    }
}
