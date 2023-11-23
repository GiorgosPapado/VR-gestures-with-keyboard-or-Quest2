using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Assets.Scripts.Evaluation.Input;

namespace Assets.Scripts.Evaluation.Views
{
    [RequireComponent(typeof(LineRenderer))]
    public class RaycastView : MonoBehaviour
    {
        private Raycaster m_Raycaster;
        private LineRenderer m_LineRenderer;

        public void Awake()
        {
            m_LineRenderer = GetComponent<LineRenderer>();
            m_LineRenderer.startWidth = 0.005f;
            m_LineRenderer.endWidth = 0.005f;
        }
        [Inject]
        public void Init(Raycaster raycaster)
        {
            m_Raycaster = raycaster;
        }

        public void Update()
        {            
            RaycastHit rayHit;            
            if (m_Raycaster.Raycast(out rayHit))            
            {
                Ray ray = m_Raycaster.GetCastingRay();                
                m_LineRenderer.SetPositions(new Vector3[] { ray.GetPoint(0), rayHit.point });
                m_LineRenderer.enabled = true;
            }
            else
            {
                m_LineRenderer.enabled = false;
            }
        }
        public void OnDisable()
        {
            m_LineRenderer.enabled = false;
        }
    }
}
