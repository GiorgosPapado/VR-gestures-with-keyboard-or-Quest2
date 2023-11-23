using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Evaluation.Input.Interface;
using UnityEngine;
using Assets.Scripts.Evaluation.Actions;
using Zenject;
using UniRx;


namespace Assets.Scripts.Evaluation.Input
{
    public class InputInterfaceSelector : IMouse2D, IAction, INavigation, IRayProvider, ITickable, IGestureAction
    {        
        List<IMouse2D> m_Mouse = new List<IMouse2D>();
        List<IAction> m_Action = new List<IAction>();
        List<INavigation> m_Navigation = new List<INavigation>();
        List<IRayProvider> m_RayProvider = new List<IRayProvider>();
        List<IGestureAction> m_GestureAction = new List<IGestureAction>();
        List<InputInterfaceName> m_InterfaceNames = new List<InputInterfaceName>();
        int m_ActiveIndex;
        int m_Count;

        KeyCode m_NextInterfaceKey = KeyCode.I;

        public InputInterfaceSelector(IList<IMouse2D> mouse, IList<IAction> action, IList<IGestureAction> gestureAction,
            IList<INavigation> navigation, IList<IRayProvider> rayProvider,
            IList<InputInterfaceName> interfaceNames)
        {            
            m_Mouse.AddRange(mouse);
            m_Action.AddRange(action);
            m_GestureAction.AddRange(gestureAction);
            m_Navigation.AddRange(navigation);
            m_RayProvider.AddRange(rayProvider);
            m_InterfaceNames.AddRange(interfaceNames);
            m_Count = m_InterfaceNames.Count;
        }

        #region IMouse2D
        public bool Valid => m_Mouse[m_ActiveIndex].Valid;

        public Vector3 GetMouse2DPosition()
        {
            return m_Mouse[m_ActiveIndex].GetMouse2DPosition();
        }
        #endregion

        #region IAction

        public bool ActionTriggered()
        {
            return m_Action[m_ActiveIndex].ActionTriggered();
        }

        public bool Cancel()
        {
            return m_Action[m_ActiveIndex].Cancel();
        }

        public bool ContextActionTriggered()
        {
            return m_Action[m_ActiveIndex].ContextActionTriggered();
        }

        public bool OK()
        {
            return m_Action[m_ActiveIndex].OK();
        }

        #endregion

        #region IGestureAction
        public bool Home()
        {
            return m_GestureAction[m_ActiveIndex].Home();
        }

        public bool Win()
        {
            return m_GestureAction[m_ActiveIndex].Win();
        }

        public bool OpenPalm()
        {
            return m_GestureAction[m_ActiveIndex].OpenPalm();
        }

        public bool GestureTriggered()
        {
            return m_GestureAction[m_ActiveIndex].GestureTriggered();
        }
        #endregion

        #region INavigation

        public float GetMoveHorizontal()
        {
            return m_Navigation[m_ActiveIndex].GetMoveHorizontal();
        }

        public float GetMoveVertical()
        {
            return m_Navigation[m_ActiveIndex].GetMoveVertical();
        }

        public float GetMoveHeight()
        {
            return m_Navigation[m_ActiveIndex].GetMoveHeight();
        }

        public float GetRotateX()
        {
            return m_Navigation[m_ActiveIndex].GetRotateX();
        }

        public float GetRotateY()
        {
            return m_Navigation[m_ActiveIndex].GetRotateY();
        }

        #endregion

        #region IRayProvider
        public Ray GetRay()
        {
            return m_RayProvider[m_ActiveIndex].GetRay();
        }
        #endregion

        #region ITickable

        public void Tick()
        {
            if(UnityEngine.Input.GetKeyDown(m_NextInterfaceKey))
            {
                do
                {
                    m_ActiveIndex++;
                    m_ActiveIndex %= m_Count;
                } while (m_InterfaceNames[m_ActiveIndex] == InputInterfaceName.ReplayWebcam);
                MessageBroker.Default.Publish(new ShowInfoOnHUDAction("Interface " + m_InterfaceNames[m_ActiveIndex].ToReadableString() + " activated.", 3.0f));
            }
        }
        #endregion

        public InputInterfaceName ActiveInputInterfaceName
        {
            get
            {
                return m_InterfaceNames[m_ActiveIndex];
            }
        }

        public void SetActiveInputInterface(InputInterfaceName interfaceName)
        {
            int index = m_InterfaceNames.IndexOf(interfaceName);
            if (index >= 0)
                m_ActiveIndex = index;
        }
    }
}
