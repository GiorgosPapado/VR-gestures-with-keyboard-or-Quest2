using Assets.Scripts.Evaluation.Input.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input
{
    public class ActionEventProvider : ITickable
    {
        private IAction m_Action;      
        public ActionEventProvider(IAction action)
        {
            m_Action = action;
        }

        public void Tick()
        {
            if(m_Action.OK())
            {
                MessageBroker.Default.Publish(new OKAction());
                Debug.Log("ACTION OK");
            }
            if(m_Action.Cancel())
            {
                MessageBroker.Default.Publish(new CancelAction());
                Debug.Log("Cancel");
            }
            if(m_Action.ActionTriggered())
            {
                MessageBroker.Default.Publish(new ActionTriggered());
                Debug.Log("Action Triggered");
            }
        }
    }
}
