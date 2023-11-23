using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Evaluation.Input.Interface;
using Assets.Scripts.Evaluation.Utils;
using UnityEngine;
using Zenject;
using UniRx;
using Assets.Scripts.Utils;
using Assets.Scripts.Evaluation.Presenters.Tasks;
using Assets.Scripts.Evaluation.Actions;
using UnityEngine.UI;


namespace Assets.Scripts.Evaluation.Input
{
    public class StdTeleportation : MonoBehaviour
    {
        //public GameObject m_Player;
        public string m_TaskTag;

        public OVRInput.Button leftControllerButton;
        public OVRInput.Button rightControllerButton;
        private IAction m_Action;
        //private Raycaster m_Raycaster;
        private Light m_CircleLightComp = null;
        public GameObject panelPrefab;
        public OVRInput.Button leftClickButton;
        public OVRInput.Button rightClickButton;
        public Camera cam;
        public Transform LeftPointerPose;
        public Transform RightPointerPose;
        public Transform LeftHandPose;
        public Transform RightHandPose;
        public GameObject TeleportPrefab;
        public GameObject player;
        //public Slider mySlider;
        readonly int layerMask = 1 << 7;
        public Rigidbody rb;

        // Start is called before the first frame update
        [Inject]
        public void Init(IAction action)//, Raycaster raycaster)
        {
            m_Action = action;
            //m_Raycaster = raycaster;
        }

        void EnableRagdoll()
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }

        void DisableRagdoll()
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        public void SpawnAtControllerPos()
        {
            bool teleportable = false;
            GameObject parent = null;
            if (OVRInput.GetDown(leftClickButton))
            {
                Ray Controllerray = new(LeftPointerPose.transform.position, LeftPointerPose.rotation * Vector3.forward);
                Ray Handray = new(LeftHandPose.transform.position, LeftHandPose.rotation * Vector3.forward);
                if (Physics.Raycast(Controllerray, out RaycastHit hit, Mathf.Infinity, layerMask) || (Physics.Raycast(Handray, out hit, Mathf.Infinity, layerMask)))
                {
                    Vector3 collision = hit.point;
                    TeleportPrefab.transform.position = collision;
                    panelPrefab.transform.position = Vector3.Lerp(cam.transform.position, TeleportPrefab.transform.position, 0.5f);
                    panelPrefab.transform.LookAt(rb.transform.position);
                    panelPrefab.transform.rotation *= Quaternion.Euler(0, 180, 0);
                    TeleportPrefab.SetActive(true);
                    panelPrefab.SetActive(true);
                    if ((parent = hit.transform.gameObject.GetParentWithTag("std_teleportation")) != null)
                    {
                        teleportable = true;
                        m_CircleLightComp = parent.GetChildWithName("Circle").GetComponent<Light>();
                        m_CircleLightComp.enabled = true;
                    }
                    else
                    {
                        teleportable = false;
                        if (m_CircleLightComp != null)
                        {
                            m_CircleLightComp.enabled = false;
                        }
                    }
                    if (teleportable && m_Action.ActionTriggered())
                    {
                        var trans = parent.GetChildWithName("TeleportationTransform").transform;
                        // Publish Task Message
                        MessageBroker.Default.Publish(new StdTeleportationCompleteAction(m_TaskTag));
                    }
                }
            }
            else if (OVRInput.GetDown(rightClickButton))
            {
                Ray Controllerray = new(RightPointerPose.transform.position, RightPointerPose.rotation * Vector3.forward);
                Ray Handray = new(RightHandPose.transform.position, RightHandPose.rotation * Vector3.forward);
                if (Physics.Raycast(Controllerray, out RaycastHit hit, Mathf.Infinity, layerMask) || (Physics.Raycast(Handray, out hit, Mathf.Infinity, layerMask)))
                {
                    Vector3 collision = hit.point;
                    TeleportPrefab.transform.position = collision;
                    panelPrefab.transform.position = Vector3.Lerp(cam.transform.position, TeleportPrefab.transform.position, 0.5f);
                    panelPrefab.transform.LookAt(rb.transform.position);
                    panelPrefab.transform.rotation *= Quaternion.Euler(0, 180, 0);
                    TeleportPrefab.SetActive(true);
                    panelPrefab.SetActive(true);
                    if ((parent = hit.transform.gameObject.GetParentWithTag("std_teleportation")) != null)
                    {
                        teleportable = true;
                        m_CircleLightComp = parent.GetChildWithName("Circle").GetComponent<Light>();
                        m_CircleLightComp.enabled = true;
                    }
                    else
                    {
                        teleportable = false;
                        if (m_CircleLightComp != null)
                        {
                            m_CircleLightComp.enabled = false;
                        }
                    }
                    if (teleportable && m_Action.ActionTriggered())
                    {
                        var trans = parent.GetChildWithName("TeleportationTransform").transform;
                        // Publish Task Message
                        MessageBroker.Default.Publish(new StdTeleportationCompleteAction(m_TaskTag));
                    }
                }
            }
        }

        public void HandTeleport()
        {
            player.transform.position = TeleportPrefab.transform.position + new Vector3(0, 1.5f, 0);
            //player.transform.Rotate(Vector3.up, mySlider.value + panelPrefab.transform.rotation.y, Space.Self);
            player.transform.Rotate(Vector3.up, panelPrefab.transform.rotation.y, Space.Self);
            TeleportPrefab.SetActive(false);
            panelPrefab.SetActive(false);
        }
        // Update is called once per frame
/*        void Update()
        {
            RaycastHit hit;
            bool teleportable = false;
            GameObject parent = null;

            if (m_Raycaster.Raycast(out hit))
            {
                if ((parent = hit.transform.gameObject.GetParentWithTag("std_teleportation")) != null)
                {
                    teleportable = true;
                    m_CircleLightComp = parent.GetChildWithName("Circle").GetComponent<Light>();
                    m_CircleLightComp.enabled = true;
                }
                else
                {
                    teleportable = false;
                    if (m_CircleLightComp != null)
                    {
                        m_CircleLightComp.enabled = false;
                    }
                }
            }
            if (teleportable && m_Action.ActionTriggered())
            {
                var trans = parent.GetChildWithName("TeleportationTransform").transform;
                m_Player.transform.position = new Vector3(trans.position.x, m_Player.transform.position.y, trans.position.z);
                m_Player.transform.rotation = trans.rotation;
                // Publish Task Message
                MessageBroker.Default.Publish(new StdTeleportationCompleteAction(m_TaskTag));
            }
        }*/
    }
}

