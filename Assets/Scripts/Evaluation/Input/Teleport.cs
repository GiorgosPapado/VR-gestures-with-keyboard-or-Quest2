using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Evaluation.Input.Interface;
using Assets.Scripts.Evaluation.Utils;
using UnityEngine;
using Zenject;
using UniRx;
using Assets.Scripts.Utils;
using Assets.Scripts.Evaluation.Actions;

namespace Assets.Scripts.Evaluation.Input
{
    public class Teleport : MonoBehaviour
    {
        public  GameObject m_Marker;
        //private IAction m_Action;
        private Vector2 m_TeleportPosition;
        //private Raycaster m_Raycaster;
        readonly int layerMask = 1 << 7;
        public Transform LeftPointerPose;
        public Transform RightPointerPose;
        public Transform LeftHandPose;
        public Transform RightHandPose;
        /*        [Inject]
                public void Init(IAction action, Raycaster raycaster)
                {
                    //m_Action = action;
                    m_Raycaster = raycaster;
                }*/

        // Update is called once per frame
        void Update()
        {            
            bool teleportable = false;
            Ray Controllerray = new(LeftPointerPose.transform.position, LeftPointerPose.rotation * Vector3.forward);
            Ray Handray = new(LeftHandPose.transform.position, LeftHandPose.rotation * Vector3.forward);
            if (Physics.Raycast(Controllerray, out RaycastHit hit, Mathf.Infinity, layerMask) || (Physics.Raycast(Handray, out hit, Mathf.Infinity, layerMask)))
            {
                if (hit.transform.gameObject.HasTagInHierarchy("floor"))
                {
                    m_Marker.SetActive(true);
                    m_Marker.transform.position = hit.point + 0.0001f * Vector3.up;
                    m_TeleportPosition = new Vector2(hit.point.x, hit.point.z);
                    teleportable = true;
                }
                else
                {
                    m_Marker.SetActive(false);
                    teleportable = false;
                }
            }
            else
            {
                m_Marker.SetActive(false);
                teleportable = false;
            }
            if (teleportable) //&& m_Action.ActionTriggered())
            {
                transform.position = new Vector3(m_TeleportPosition.x, transform.position.y, m_TeleportPosition.y);
                MessageBroker.Default.Publish(new TeleportCompleteAction());
            }
        }
    }
}