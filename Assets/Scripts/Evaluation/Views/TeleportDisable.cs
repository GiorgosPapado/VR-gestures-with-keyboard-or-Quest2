using Assets.Scripts.Evaluation.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Evaluation.Views
{
    public class TeleportDisable : MonoBehaviour
    {
        private Teleport m_Teleport;
        private bool m_TeleportWasActive = false;
        [Inject]
        public void Init(Teleport teleport)
        {
            m_Teleport = teleport;
        }
        private void OnDisable()
        {
            m_Teleport.enabled = m_TeleportWasActive;
        }
        private void OnEnable()
        {
            m_TeleportWasActive = m_Teleport.enabled;
            m_Teleport.enabled = false;
        }
    }
}
