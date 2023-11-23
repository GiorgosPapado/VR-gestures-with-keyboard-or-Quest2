using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using Zenject;
using Assets.Scripts.Evaluation.Model.Tasks;
using Assets.Scripts.Evaluation.Views.Tasks;
using Assets.Scripts.Evaluation.Actions;

namespace Assets.Scripts.Evaluation.Presenters.Tasks
{
    public class TaskManagerPresenter : MonoBehaviour, IInitializable
    {
        public GameObject m_Content;
        public GameObject m_InitialState;

        private List<TaskView> m_Tasks = new List<TaskView>();

        private IDisposable m_Sub;

        private int m_CurrentTaskID = 0;

        private bool m_Enabled = false;
        private void EndCurrentTask()
        {
            MessageBroker.Default.Publish(new ShowInfoOnHUDAction(
                TaskTutorialConstantStrings.All[m_CurrentTaskID + 1]      // +1 to skip the welcome message
                ));

            m_Tasks[m_CurrentTaskID].gameObject.SetActive(false);
        }
        private void EnableTaskTracking()
        {
            m_CurrentTaskID = 0;
            m_Sub = MessageBroker.Default.Receive<TaskComplete>().Subscribe(completedTask =>
            {
                EndCurrentTask();
                if(m_CurrentTaskID < m_Tasks.Count)
                {
                    m_CurrentTaskID++;
                    BeginTask(m_Tasks[m_CurrentTaskID]);
                    if(m_CurrentTaskID == m_Tasks.Count-1)
                    {
                        m_Tasks.Last().gameObject.SetActive(false);
                        m_Enabled = false;
                    }
                }
                else
                {
                    m_CurrentTaskID = 0;
                    m_Sub.Dispose();        // stop tracking tasks                    
                }                               
            });

            // First Task is TaskInitialTeleport (first in TaskView list)
            // Enable/disable corresponding objects of current task view
            // Broadcast first notification to teleport to arbitrary location
            
            BeginTask(m_Tasks[m_CurrentTaskID]);
            MessageBroker.Default.Publish(new ShowInfoOnHUDAction(
                TaskTutorialConstantStrings.Welcome));
        }
        private void DisableAll(List<TaskView> taskViews)
        {
            taskViews.ForEach(x => x.gameObject.SetActive(false));
        }
        public void Initialize()
        {
            m_Tasks.AddRange(m_Content.GetComponentsInChildren<TaskView>(true));        // check order of retrieval

            if (m_Enabled) 
            { 
                EnableTaskTracking();
            }
        }

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.T))
            {
                m_Enabled = !m_Enabled;
                if (m_Enabled)
                {
                    EnableTaskTracking();
                }
                else
                {
                    m_Sub.Dispose();
                    // Enable last task (which is the free interaction)
                    BeginTask(m_Tasks.Last());
                    DisableAll(m_Tasks);
                    MessageBroker.Default.Publish(new ShowInfoOnHUDAction("Tutorial level disabled", 3.0f));                    
                }
            }
        }

        /// @brief Enables and disables the corresponding objects of the specified taskView
        /// @param taskView The `TaskView` to start.
        private void BeginTask(TaskView taskView)
        {
            foreach (GameObject gobj in taskView.m_EnableObjects)
            {
                gobj.SetActive(true);
            }

            foreach (GameObject gobj in taskView.m_DisableObjects)
            {
                gobj.SetActive(false);
            }
            taskView.gameObject.SetActive(true);        // enable task presenter
        }
    }
}
