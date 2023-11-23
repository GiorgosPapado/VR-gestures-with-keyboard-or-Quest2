using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Utils
{
    public abstract class Activatable : IActivatable
    {
        private bool m_Enabled;

        protected abstract void OnEnable();
        protected abstract void OnDisable();

        public bool Enabled
        {
            get
            {
                return m_Enabled;
            }
            set
            {
                if(m_Enabled != value)
                {
                    m_Enabled = value;
                    if (m_Enabled)
                    {
                        OnEnable();
                    }
                    else
                    {
                        OnDisable();
                    }
                }
            }
        }
    }
}
