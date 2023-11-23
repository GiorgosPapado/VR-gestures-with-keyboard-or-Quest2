using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Actions
{
    public class ShowVideoTaskAction
    {
        public int ID { get; set; }
        public string Tag = "VideoPlayerOpenedTask";
        public ShowVideoTaskAction(int ID)
        {
            this.ID = ID;
        }
    }
}