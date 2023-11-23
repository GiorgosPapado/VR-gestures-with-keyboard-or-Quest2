using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Input.Replay
{
    [MessagePackObject]
    public class InputState
    {
        #region IAction
        [Key("ActionTriggered")]
        public bool ActionTriggered { get; set; }
        [Key("ContextActionTriggered")]
        public bool ContextActionTriggered { get; set; }
        [Key("OK")]
        public bool OK { get; set; }
        [Key("Cancel")]
        public bool Cancel { get; set; }
        #endregion

        #region IGestureAction
        [Key("Home")]
        public bool Home { get; set; }
        [Key("Win")]
        public bool Win { get; set; }
        [Key("OpenPalm")]
        public bool OpenPalm { get; set; }
        [Key("GestureTriggered")]
        public bool GestureTriggered { get; set; }
        #endregion

        #region IMouse2D
        [Key("Mous2DPosition")]
        public Vector2 Mouse2DPosition {get;set;}
        [Key("Valid")]
        public bool Valid { get; set; }
        #endregion

        #region INavigation
        [Key("MoveHorizontal")]
        public float MoveHorizontal { get; set; }
        [Key("MoveVertical")]
        public float MoveVertical { get; set; }
        [Key("MoveHeight")]
        public float MoveHeight { get; set; }
        [Key("RotateX")]
        public float RotateX { get; set; }
        [Key("RotateY")]
        public float RotateY { get; set; }
        #endregion
    }
}
