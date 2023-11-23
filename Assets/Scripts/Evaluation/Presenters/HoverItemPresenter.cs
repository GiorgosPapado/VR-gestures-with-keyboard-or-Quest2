using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Input;

namespace Assets.Scripts.Evaluation.Presenters
{   
    [RequireComponent(typeof(Collider))]
    public class HoverItemPresenter : MonoBehaviour
    {
        public  GameObject m_HoverItem;
        public  Vector3 m_ViewOffset;
        //private Raycaster m_Raycaster;
        //public OVRInput.Button leftClickButton;
        //public OVRInput.Button rightClickButton;
        public Camera cam;
        public Transform LeftPointerPose;
        public Transform RightPointerPose;
        public Transform LeftHandPose;
        public Transform RightHandPose;
        readonly int layerMask = 1 << 7;
        /*        [Inject]
                void Init(Raycaster raycaster)
                {
                    m_Raycaster = raycaster;
                }
        */
        // Update is called once per frame
        void Update()
        {
            Ray Controllerray = new(LeftPointerPose.transform.position, LeftPointerPose.rotation * Vector3.forward);
            Ray Handray = new(LeftHandPose.transform.position, LeftHandPose.rotation * Vector3.forward);
            if (Physics.Raycast(Controllerray, out RaycastHit hit, Mathf.Infinity, layerMask) || (Physics.Raycast(Handray, out hit, Mathf.Infinity, layerMask)))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    m_HoverItem.transform.position = hit.point + m_ViewOffset;
                    m_HoverItem.SetActive(true);
                    if (m_HoverItem.tag == "MetroStationHoveredTaskTag")
                    {
                        MessageBroker.Default.Publish(new MetroStationHoveredTaskAction(m_HoverItem.tag));
                    }
                }
                else
                {
                    m_HoverItem.SetActive(false);
                }
            }
            else
            {
                m_HoverItem.SetActive(false);
            }
        }
    }
}
