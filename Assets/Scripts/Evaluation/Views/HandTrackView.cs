using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Input.Interface;

namespace Assets.Scripts.Evaluation.Views
{
    public class HandTrackView : MonoBehaviour
    {
        public GameObject m_Marker;

        private IRayProvider m_RayProvider;

        [Inject]
        public void Init(IRayProvider rayProvider)
        {
            m_RayProvider = rayProvider;
        }
        public void Update()
        {
            Ray ray = m_RayProvider.GetRay();
            if(ray.direction != Vector3.zero)
            {
                m_Marker.transform.position = ray.GetPoint(0.1f);
                m_Marker.SetActive(true);                
            }
            else
            {
                m_Marker.SetActive(false);
            }
        }
    }
}
