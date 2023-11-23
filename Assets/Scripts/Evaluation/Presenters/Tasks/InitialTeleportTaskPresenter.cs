using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Model.Tasks;
using UniRx;
namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class InitialTeleportTaskPresenter : MonoBehaviour
    {
        public int ID;
        IDisposable m_Sub;
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<TeleportCompleteAction>().Subscribe(_ =>
            {
                TaskComplete.Notify(ID, TaskType.TELEPORTATION);
            });
        }
        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}
