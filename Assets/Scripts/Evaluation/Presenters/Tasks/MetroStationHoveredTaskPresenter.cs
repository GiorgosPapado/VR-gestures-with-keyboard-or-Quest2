using System.Collections;
using UnityEngine;
using UniRx;
using System;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Model.Tasks;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class MetroStationHoveredTaskPresenter : MonoBehaviour
    {

        public int ID;
        public string m_MetroStationHoveredTaskTag;
        private IDisposable m_Sub;
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<MetroStationHoveredTaskAction>().Where(x => x.Tag == m_MetroStationHoveredTaskTag).Subscribe(_ =>
            {
                TaskComplete.Notify(ID, TaskType.METRO_STATION_HOVERED);
            });
        }

        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}