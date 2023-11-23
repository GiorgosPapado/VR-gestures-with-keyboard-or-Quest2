using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.InteropServices;
using MessagePack;
namespace Assets.Scripts.Evaluation.Gestures
{
    [MessagePackObject]
    public class HandData
    {
        [MessagePackObject]
        public class Hand
        {
            [Key("Landmarks")]
            public Vector3[] Landmarks { get; } = new Vector3[(int)HandLandmark.TOTAL];
            [Key("Handedness")]
            public Handedness Handedness { get; set; }
            [Key("HandednessConfidence")]
            public float HandednessConfidence { get; set; }
        }
        
        private List<Hand> m_Hands = new List<Hand>();
        
        public HandData()           // for messagepack
        {

        }
        public HandData(LibHands.HandData handData)
        {
            if (handData.nHands == 0)
                return;

            int[] handedness = new int[handData.nHands];
            float[] handedness_conf = new float[handData.nHands];
            float[] landmarks = new float[3 * (int)HandLandmark.TOTAL * handData.nHands];
            
            Marshal.Copy(handData.pHandedness, handedness, 0, handedness.Length);
            Marshal.Copy(handData.pHandednessConfidence, handedness_conf, 0, handedness_conf.Length);
            Marshal.Copy(handData.pLandmarks, landmarks, 0, landmarks.Length);

            for(int hand = 0; hand<handData.nHands;hand++)
            {
                Hand h = new Hand();

                h.Handedness = (Handedness)handedness[hand];
                h.HandednessConfidence = handedness_conf[hand];
                for(int lmark=0; lmark<(int)HandLandmark.TOTAL;lmark++)
                {
                    int baseindex = hand * 3 * (int)HandLandmark.TOTAL + 3 * lmark;
                    Vector3 mark = new Vector3(landmarks[baseindex + 0], landmarks[baseindex + 1], landmarks[baseindex + 2]);
                    h.Landmarks[lmark] = mark;
                }

                m_Hands.Add(h);
            }    
        }

        [Key("Hands")]
        public List<Hand> Hands
        {
            get
            {
                return m_Hands;
            }
            set
            {
                m_Hands = value;
            }
        }
    }
}
