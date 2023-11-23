using System.Collections;
using UnityEngine;
using UniRx;
using System;
using Assets.Scripts.Evaluation.Model.Tasks;
using Assets.Scripts.Evaluation.Actions;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class FilesystemIconClickedTaskPresenter : MonoBehaviour
    {

        public int ID;
        public string m_FilesystemClickedTaskTag;
        private IDisposable m_Sub;
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<FilesystemButtonClickedTaskAction>().Where(x => x.Tag == m_FilesystemClickedTaskTag).Subscribe(_ =>
            {
                TaskComplete.Notify(ID, TaskType.FILESYSTEM_BUTTON_CLICKED);
            });
        }

        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}