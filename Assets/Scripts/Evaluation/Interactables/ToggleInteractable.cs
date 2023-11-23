using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Evaluation.Interactables
{
    [RequireComponent(typeof(BoxCollider))]
    public class ToggleInteractable : MonoBehaviour, IInteractable
    {
        public Toggle m_Toggle;

        public void Awake()
        {
            if (m_Toggle is null)
            {
                m_Toggle = GetComponent<Toggle>();
            }
        }

        public void OnInvokeInteraction()
        {
            if (m_Toggle != null)
            {
                m_Toggle.isOn = !m_Toggle.isOn;
            }
        }

        public void OnHoverEnter()
        {
            if (m_Toggle != null)
            {
                ExecuteEvents.Execute(m_Toggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }

        public void OnHoverExit()
        {
            if (m_Toggle != null)
            {
                ExecuteEvents.Execute(m_Toggle.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
            }
        }
    }
}
