using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Gestures.Filters
{
    public class TimeIntervalFilter<T>
    {
        private float m_StartTimestamp = 0.0f;
        private bool m_IsFirst = true;

        public void Reset()
        {
            m_IsFirst = true;
            m_StartTimestamp = 0.0f;
            IsContinuouslyTheSameValue = false;
        }


        public void Update(T value, float timestamp)
        {
            if(m_IsFirst)
            {
                m_StartTimestamp = timestamp;
                m_IsFirst = false;
                IsContinuouslyTheSameValue = false;
                Value = value;
            }
            else
            {
                if(Value.Equals(value))
                {
                    if(timestamp >= FilterInterval)
                    {
                        IsContinuouslyTheSameValue = true;
                    }
                }
                else
                {
                    IsContinuouslyTheSameValue = false;
                    m_StartTimestamp = timestamp;
                    Value = value;
                }
            }
        }

        public float FilterInterval { get; set; } = 1.0f;
        public bool IsContinuouslyTheSameValue { get; private set; } = false;
        public T Value { get; private set; }
     }
}
