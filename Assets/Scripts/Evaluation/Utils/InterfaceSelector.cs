using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Utils
{
    public class InterfaceSelector<T> 
    {
        private IList<T> m_Interfaces;
        private int m_ActiveIndex = 0;
        public InterfaceSelector(IList<T> interfaces)
        {
            m_Interfaces = interfaces;
        }

        public T GetActiveInterface()
        {
            return m_Interfaces[m_ActiveIndex];
        }

        public void SwitchToNextInterface()
        {
            m_ActiveIndex = (m_ActiveIndex + 1) % m_Interfaces.Count;
        }
        public void SwitchToPreviousIterface()
        {
            m_ActiveIndex--;
            if (m_ActiveIndex < 0)
                m_ActiveIndex += m_Interfaces.Count;
        }
    }
}
