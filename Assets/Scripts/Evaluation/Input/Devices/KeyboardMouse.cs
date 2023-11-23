using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Evaluation.Input.Interface;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input.Devices
{
    public class KeyboardMouse : IAction, IMouse2D, INavigation, IGestureAction
    {
        public KeyCode MoveCameraButton { get; set; } = KeyCode.C;
        public KeyCode MoveUpButton { get; set; } = KeyCode.PageUp;
        public KeyCode MoveDownButton { get; set; } = KeyCode.PageDown;

        public bool Valid => true;

        public bool ActionTriggered()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }

        public bool ContextActionTriggered()
        {
            return UnityEngine.Input.GetMouseButtonDown(1);
        }

        public Vector3 GetMouse2DPosition()
        {
            return UnityEngine.Input.mousePosition;
        }

        public float GetMoveHorizontal()
        {
            return UnityEngine.Input.GetAxis("Horizontal");
        }

        public float GetMoveVertical()
        {
            return UnityEngine.Input.GetAxis("Vertical");
        }

        public float GetMoveHeight()
        {
            float r = 0.0f;
            bool up = UnityEngine.Input.GetKey(MoveUpButton);
            bool down = UnityEngine.Input.GetKey(MoveDownButton);
            r += up ? 0.5f : 0.0f;
            r -= down ? 0.5f : 0.0f;
            return r;
        }

        public float GetRotateX()
        {
            if (UnityEngine.Input.GetKey(MoveCameraButton))
            {
                return UnityEngine.Input.GetAxis("Mouse X");
            }
            else
            {
                return 0.0f;
            }
        }

        public float GetRotateY()
        {
            if (UnityEngine.Input.GetKey(MoveCameraButton))
            {
                return UnityEngine.Input.GetAxis("Mouse Y");
            }
            else
            {
                return 0.0f;
            }
        }

        public bool OK()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Return);
        }
        public bool Cancel()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Escape);
        }

        public bool Home()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Home);
        }

        public bool Win()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.E);
        }

        public bool OpenPalm()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.E);
        }

        public bool GestureTriggered()
        {
            return false;
        }
    }
}
