using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input
{
    public class AppWindowMode : MonoBehaviour
    {
        public void Update()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.F) && UnityEngine.Input.GetKey(KeyCode.LeftAlt))
            {
                Screen.fullScreen = true;
                
            }
            else if(UnityEngine.Input.GetKeyDown(KeyCode.W) && UnityEngine.Input.GetKey(KeyCode.LeftAlt))
            {
                Screen.fullScreen = false;
            }
        }
    }
}
