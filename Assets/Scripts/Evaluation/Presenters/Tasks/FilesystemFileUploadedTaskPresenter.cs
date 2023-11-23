using System.Collections;
using UnityEngine;
using UniRx;
using System;
using Assets.Scripts.Evaluation.Model.Tasks;
using Assets.Scripts.Evaluation.Actions;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class FilesystemFileUploadedTaskPresenter : MonoBehaviour
    {

        public int ID;
        public string m_FilesystemClickedTaskTag;
        private IDisposable m_Sub;
        public void OnEnable()
        {
            m_Sub = MessageBroker.Default.Receive<FileUploadedTaskAction>().Where(x => x.Tag == m_FilesystemClickedTaskTag).Subscribe(_ =>
            {
                TaskComplete.Notify(ID, TaskType.FILESYSTEM_FILE_UPLOADED);
            });
        }

        public void OnDisable()
        {
            m_Sub.Dispose();
        }
    }
}