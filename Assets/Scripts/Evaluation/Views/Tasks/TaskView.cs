using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using UniRx;

namespace Assets.Scripts.Evaluation.Views.Tasks
{
    public class TaskView : MonoBehaviour
    {
        public GameObject[] m_EnableObjects;
        public GameObject[] m_DisableObjects;

        public IDisposable m_Sub;
        public void Start()
        {

        }
    }

    
}
