using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Evaluation.Utils
{
    // allows only one pop per frame
    public class TopWidgetTracker : ITickable
    {
        private Stack<GameObject> m_Widgets = new Stack<GameObject>();
        private bool m_AllowPop = true;
        public void PushWidget(GameObject widget)
        {
            // make sure the widget is not already active
            // otherwise if we close it, we have a leak.
            // This widget is not visible to close it a second time and remove
            // previous instances on the stack
            if (!m_Widgets.ToList().Contains(widget))           
                m_Widgets.Push(widget);
            widget.SetActive(true);
        }

        public void PopWidget(GameObject widget = null)
        {
            if (!m_AllowPop)
                return;

            if(m_Widgets.Count > 0)
            {
                if (widget == null)
                {
                    var w = m_Widgets.Pop();
                    w.SetActive(false);
                    m_AllowPop = false;
                }
                else
                {
                    if(m_Widgets.Peek() == widget)
                    {
                        m_Widgets.Pop();
                        widget.SetActive(false);
                        m_AllowPop = false;
                    }
                }
            }
        }
        public bool IsTop(GameObject widget)
        {
            if (!m_AllowPop)
                return false;

            if (m_Widgets.Count > 0)
            {
                return m_Widgets.Peek() == widget;
            }
            else 
            { 
                return false;
            }
        }

        public void Tick()
        {
            m_AllowPop = true;
        }
    }
}
