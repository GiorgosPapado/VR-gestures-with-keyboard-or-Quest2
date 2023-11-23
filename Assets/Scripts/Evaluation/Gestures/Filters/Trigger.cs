using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Gestures.Filters
{
    public class Trigger<T>
    {
        private T m_LastValue;

        public bool Triggered { get; private set; } = false;

        public Trigger(T initialValue)
        {
            m_LastValue = initialValue;
        }

        public bool Update(T value)
        {
            Triggered = !m_LastValue.Equals(value);
            m_LastValue = value;
            return Triggered;
        }
    }
}
