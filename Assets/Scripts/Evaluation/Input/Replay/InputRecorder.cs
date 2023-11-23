using Assets.Scripts.Evaluation.Input.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Input.Replay
{
    public class InputRecorder
    {
        private IAction m_Action;
        private IGestureAction m_GestureAction;
        private IMouse2D m_Mouse2D;
        private INavigation m_Navigation;

        public InputRecorder(IAction action, IGestureAction gestureAction, IMouse2D mouse2D, INavigation navigation)
        {
            m_Action = action;
            m_GestureAction = gestureAction;
            m_Mouse2D = mouse2D;
            m_Navigation = navigation;
        }

        public InputState GetState()
        {
            InputState state = new InputState();
            // IAction
            state.ActionTriggered = m_Action.ActionTriggered();
            state.ContextActionTriggered = m_Action.ContextActionTriggered();
            state.OK = m_Action.OK();
            state.Cancel = m_Action.Cancel();
            // IGestureAction
            state.Home = m_GestureAction.Home();
            state.Win = m_GestureAction.Win();
            state.OpenPalm = m_GestureAction.OpenPalm();
            state.GestureTriggered = m_GestureAction.GestureTriggered();
            // IMouse2D
            state.Mouse2DPosition = m_Mouse2D.GetMouse2DPosition();
            state.Valid = m_Mouse2D.Valid;
            // INavigation
            state.MoveHorizontal = m_Navigation.GetMoveHorizontal();
            state.MoveVertical = m_Navigation.GetMoveVertical();
            state.MoveHeight = m_Navigation.GetMoveHeight();
            state.RotateX = m_Navigation.GetRotateX();
            state.RotateY = m_Navigation.GetRotateY();           
            return state;
        }
    }
}
