using System;
using System.Collections;
using UnityEngine;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Model.Tasks;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class FacebookWidgetClickTaskPresenter : MonoBehaviour
    {
        public int ID;
        public string m_FacebookWidgetClickTag;
        private IDisposable m_Sub;

        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<FacebookWidgetClickAction>().Where(x => x.Tag == m_FacebookWidgetClickTag).Subscribe(_ => 
            {
                TaskComplete.Notify(ID, TaskType.FACEBOOK_BUTTON_CLICKED);
            });
        }

        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}