using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Model.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class TeleportHomeTaskPresenter : MonoBehaviour
    {
        public int ID;
        private IDisposable m_Sub;
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<TeleportHomeAction>().Subscribe(_ =>
            {
                TaskComplete.Notify(ID, TaskType.TELEPORT_HOME);
            });
        }

        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}
