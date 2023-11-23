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
    public class EntityItemPinnedTaskPresenter : MonoBehaviour
    {
        public int ID;
        private IDisposable m_Sub;
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<EntityItemPinned>().Subscribe(_ =>
            {
                TaskComplete.Notify(ID, TaskType.ENTITY_ITEM_PINNED_ON_MAP);
            });
        }
    }
}
