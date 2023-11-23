
using System.Collections;
using UnityEngine;
using UniRx;
using Assets.Scripts.Evaluation.Model.Tasks;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class StdTeleportLocationPresenterFilesystem : MonoBehaviour
    {

        public class StdTeleportLocationPresenter : MonoBehaviour, ITaskNotify
        {
            public int ID;

            public void Notify()
            {
                MessageBroker.Default.Publish(new TaskComplete(ID, TaskType.STANDARD_TELEPORTATION_FILESYSTEM));
            }
        }
    }
}