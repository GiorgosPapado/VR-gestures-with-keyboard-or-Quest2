using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts.Evaluation.Input
{
    public class AppQuit : MonoBehaviour
    {
        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q) && UnityEngine.Input.GetKey(KeyCode.LeftAlt))
            {
                Application.Quit();
            }
        }
    }
}
