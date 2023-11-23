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
    public class ButtonInteractable : MonoBehaviour, IInteractable
    {
        public Button m_Button;        

        public void Awake()
        {
            if(m_Button is null)
            {
                m_Button = GetComponent<Button>();
            }        
        }

        public void OnInvokeInteraction()
        {
            if(m_Button != null)
            {
                ExecuteEvents.Execute(m_Button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }            
        }

        public void OnHoverEnter()
        {
            if(m_Button != null)
            {
                ExecuteEvents.Execute(m_Button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }

        public void OnHoverExit()
        {
            if (m_Button != null)
            {
                ExecuteEvents.Execute(m_Button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
            }
        }        
    }
}
