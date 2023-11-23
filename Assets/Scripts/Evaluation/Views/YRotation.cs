using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Views
{
    public class YRotation : MonoBehaviour
    {
        public int rotationSpeed = 50;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

}
