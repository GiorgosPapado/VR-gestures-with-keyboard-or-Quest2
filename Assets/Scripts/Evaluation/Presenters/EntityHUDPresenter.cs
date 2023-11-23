using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UniRx;
using UnityEngine;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Input.Interface;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class EntityHUDPresenter : ITickable
    {
        private IGestureAction m_GestureAction;

        private bool m_EntityHUDOnWinGesture = true;
        private bool m_EntityHUDOnPalmGesture = false;

        public EntityHUDPresenter(IGestureAction gestureAction)
        {
            m_GestureAction = gestureAction;
        }

        public void Tick()
        {
            if ((m_EntityHUDOnWinGesture && m_GestureAction.Win())
                || (m_EntityHUDOnPalmGesture && m_GestureAction.OpenPalm()))
            {
                MessageBroker.Default.Publish(new ShowEntitySystemAction());
            }

            if(UnityEngine.Input.GetKeyDown(KeyCode.Keypad0))
            {
                m_EntityHUDOnWinGesture = !m_EntityHUDOnWinGesture;
            }
            if(UnityEngine.Input.GetKeyDown(KeyCode.Keypad1))
            {
                m_EntityHUDOnPalmGesture = !m_EntityHUDOnPalmGesture;
            }
        }
    }
}
