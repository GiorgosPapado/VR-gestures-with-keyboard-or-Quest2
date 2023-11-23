using Assets.Scripts.Evaluation.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Evaluation.Mock
{
    public class MockEntityHUD : ITickable
    {
        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                MessageBroker.Default.Publish(new ShowEntitySystemAction());
            }
        }
    }
}
