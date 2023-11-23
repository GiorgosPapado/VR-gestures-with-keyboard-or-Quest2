using Assets.Scripts.Evaluation.Input.Interface;
using Assets.Scripts.Evaluation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Evaluation.Input
{
    public class Raycaster : MonoBehaviour                          // MonoBehaviour in order to allow config of order of execution script
    {
        private IRayProvider m_RayProvider;
        private bool m_CacheNextHit = true;
        private RaycastHit m_CachedHit;
        private bool m_CachedHitResult = false;
        private bool m_CacheNextRay = true;
        private Ray m_CachedRay;
        
        [Inject]
        public void Init(IRayProvider rayProvider)
        {
            m_RayProvider = rayProvider;
        }
        public Ray GetCastingRay()
        {            
            if(m_CacheNextRay)
            {
                Ray ray = m_RayProvider.GetRay();
                m_CachedRay = ray;
                m_CacheNextRay = false;
                return m_CachedRay;
            }
            else
            {
                return m_CachedRay;
            }            
        }
        public bool Raycast(out RaycastHit hit)
        {
            if(m_CacheNextHit)
            {
                Ray ray = GetCastingRay();
                m_CachedHitResult =  Physics.Raycast(ray, out hit);
                m_CachedHit = hit;
                m_CacheNextHit = false;
                return m_CachedHitResult;
            }
            else
            {
                hit = m_CachedHit;
                return m_CachedHitResult;
            }
        }

        public void Update()
        {
            // this script's execution priority is high
            m_CacheNextHit = true;
            m_CacheNextRay = true;
        }
    }
}
