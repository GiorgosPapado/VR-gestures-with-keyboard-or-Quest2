using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Evaluation.Interactables
{
    [RequireComponent(typeof(BoxCollider))]
    public class ColliderFit : MonoBehaviour
    {
        public float m_Zsize = 50.0f;
        private void Start()
        {
            var trans = gameObject.GetComponent<RectTransform>();
            var col = gameObject.GetComponent<BoxCollider>();
            col.center = new Vector3(trans.rect.center.x, trans.rect.center.y, 0.0f);
            col.size = new Vector3(trans.rect.size.x, trans.rect.size.y, m_Zsize);
        }
    }
}
