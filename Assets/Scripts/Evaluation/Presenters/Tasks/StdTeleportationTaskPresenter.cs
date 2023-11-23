using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Model.Tasks;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class StdTeleportationTaskPresenter : MonoBehaviour
    {
        public int ID;
        public string m_TeleportLocationTag;
        private IDisposable m_Sub;
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<StdTeleportationCompleteAction>().Where(x=>x.Tag == m_TeleportLocationTag).Subscribe(_ =>
            {
                TaskComplete.Notify(ID, TaskType.STANDARD_TELEPORTATION);
            });
        }

        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}

