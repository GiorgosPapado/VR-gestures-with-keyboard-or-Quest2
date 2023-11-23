using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using UnityEngine;
using UnityEngine.UIElements;
namespace Assets.Scripts.Evaluation.Input.Replay
{
    [MessagePackObject]
    public class KeyboardMouseState
    {        
        [Key("KeyDownEvent")]
        public List<KeyCode> KeyDownEvent { get; set; } = new List<KeyCode>();

        [Key("KeyUpEvent")]
        public List<KeyCode> KeyUpEvent { get; set; } = new List<KeyCode>();

        [Key("MouseDownEvent")]
        public List<int> MouseButonDownEvent { get; set; } = new List<int>();

        [Key("MouseUpEvent")]
        public List<int> MouseButtonUpEvent { get; set; } = new List<int>();
        [Key("KeyModifiers")]
        public EventModifiers KeyModifiers { get; set; } = EventModifiers.None;

        [Key("MousePosition")]
        public Vector3 MousePosition { get; set; } = Vector3.zero;

        [Key("ScreenResolution")]
        public Vector2 ScreenResolution { get; set; } = Vector2.zero;
    }
}
