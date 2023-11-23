using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Model.Tasks;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class EntityHUDItemSelectedTaskPresenter :  MonoBehaviour
    {
        public int ID;
        private IDisposable m_Sub;
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<AddEntityOnMapAction>().Subscribe(_ =>
            {
                TaskComplete.Notify(ID, TaskType.ENTITY_ITEM_SELECTED_FROM_HUD);
            });       
        }

        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}
