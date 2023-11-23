using Assets.Scripts.Evaluation.Input.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public enum RaycastType
    {
        LeftController,
        RightController
    }

    public class VRControllerRayProvider : MonoBehaviour, IRayProvider
    {
        public GameObject m_RightController;
        public GameObject m_LeftController;

        public RaycastType RaycastType { get; set; } = RaycastType.RightController;

        public Ray GetRay()
        {
            Transform trans = RaycastType == RaycastType.RightController ? m_RightController.transform : m_LeftController.transform;
            Ray ray = new Ray(trans.position, trans.rotation * Vector3.forward);
            return ray;
        }
    }
}
