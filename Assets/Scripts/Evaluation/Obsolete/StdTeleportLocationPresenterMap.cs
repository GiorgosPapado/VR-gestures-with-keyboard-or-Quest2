//using Assets.Scripts.Evaluation.Model.Tasks;
//using System.Collections;
//using UniRx;
//using UnityEngine;

//namespace Assets.Scripts.Evaluation.Presenters.Tasks
//{
//    public class StdTeleportLocationPresenterMap : MonoBehaviour
//    {

//        public class StdTeleportLocationPresenter : MonoBehaviour, ITaskNotify
//        {
//            public int ID;

//            public void Notify()
//            {
//                MessageBroker.Default.Publish(new TaskComplete(ID, TaskType.STANDARD_TELEPORTATION_MAP));
//            }
//        }
//    }
//}