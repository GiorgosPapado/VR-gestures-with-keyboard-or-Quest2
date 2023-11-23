using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Input;
using UnityEngine;
using Assets.Scripts.Evaluation.Utils;
using Assets.Scripts.Evaluation.Input.Interface;

namespace Assets.Scripts.Evaluation.Interactables
{
    public class InteractionInvoker : Activatable, IInitializable, ITickable
    {
        private Raycaster m_Raycaster;
        private IAction m_Action;

        private RaycastHit m_LastHit;
        private bool m_LastHitResult;

        protected override void OnEnable()
        {
            
        }

        protected override void OnDisable()
        {
            if(m_LastHitResult)
            {
                m_LastHit.transform.gameObject.GetComponent<IInteractable>()?.OnHoverExit();
            }
            m_LastHitResult = false;            
        }

        public void Initialize()
        {
            //Enabled = false;
            Enabled = true;
        }

        public InteractionInvoker(Raycaster raycaster, IAction action)
        {
            m_Raycaster = raycaster;
            m_Action = action;
        }

        public void Tick()
        {
            if(Enabled)
            {
                RaycastHit hit;
                if (m_Raycaster.Raycast(out hit))
                {
                    if(m_LastHitResult && hit.transform?.gameObject != m_LastHit.transform?.gameObject)
                    {
                        m_LastHit.transform?.gameObject?.GetComponent<IInteractable>()?.OnHoverExit();
                    }
                    if (m_Action.ActionTriggered())
                    {
                        hit.transform?.gameObject?.GetComponent<IInteractable>()?.OnInvokeInteraction();
                    }
                    else
                    {
                        hit.transform?.gameObject?.GetComponent<IInteractable>()?.OnHoverEnter();
                    }
                    m_LastHitResult = true;
                    m_LastHit = hit;
                }
                else
                {
                    m_LastHit.transform?.gameObject?.GetComponent<IInteractable>()?.OnHoverExit();
                    m_LastHitResult = false;
                }
            }
        }

    }
}
