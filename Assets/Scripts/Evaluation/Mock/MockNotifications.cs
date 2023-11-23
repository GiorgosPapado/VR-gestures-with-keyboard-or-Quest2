using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Actions;

namespace Assets.Scripts.Evaluation.Mock
{
    public class MockNotifications : ITickable
    {
        private int m_ID = 0;
        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.N))
            {
                MessageBroker.Default.Publish(new ShowInfoOnHUDAction("Test notification #" + (m_ID++).ToString(), 3.0f));
            }
        }
    }
}
