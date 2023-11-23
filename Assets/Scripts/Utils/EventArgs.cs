using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public class EventArgs<T> : EventArgs
    {
        private T m_Value;

        public EventArgs(T value)
        {
            m_Value = value;
        }

        public T Value
        {
            get
            {
                return m_Value;
            }            
        }
    }
}
