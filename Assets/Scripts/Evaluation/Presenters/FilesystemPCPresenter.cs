using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Evaluation.Actions;
using UniRx;
namespace Assets.Scripts.Evaluation.Presenters
{
    public class FilesystemPCPresenter : MonoBehaviour
    {
        public Button m_FilesystemButton;

        public void Awake()
        {
            m_FilesystemButton.OnClickAsObservable().Subscribe(_ =>
            {
                MessageBroker.Default.Publish(new ShowFileSystemAction());
                MessageBroker.Default.Publish(new FilesystemButtonClickedTaskAction("FilesystemButtonTaskTag"));
            });
        }

    }
}
