using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.OnnxRuntime;
using Zenject;
using Assets.Scripts.Evaluation.Utils;
using Assets.Scripts.Evaluation.Gestures.Interface;
using UnityEngine;

namespace Assets.Scripts.Evaluation.Gestures.ONNX
{
    public enum HandGestureType
    {
        Back,
        Home,
        Negative,
        OK,
        OpenPalm,
        Point,
        PreSelect,
        Select,
        Win,
        None
    }
    public class HandGestures
    {
        private InferenceSession m_Session;        
        private IDisposable m_Sub;
        public Handedness Handedness { get; set; } = Handedness.RIGHT;
        private Handedness OppositeHandedness
        {
            get
            {
                switch (Handedness)
                {
                    case Handedness.RIGHT:
                        return Handedness.LEFT;
                    case Handedness.LEFT:
                        return Handedness.RIGHT;
                    default:
                        return Handedness.LEFT;
                }
            }
        }
        public HandGestures()
        {
            m_Session = new InferenceSession(".\\gestures\\hand-gestrec.onnx");
        }

        public HandGestureType Inference(HandData handData)
        {
            var hand = handData?.Hands.Where(x => x.Handedness == Handedness).FirstOrDefault();
            if (hand is null)
            {
                return HandGestureType.None;
            }
            Tensor<float> inputTensor = new DenseTensor<float>(new int[] { 1, 21 * 3 });
            int p = 0;
            Vector3 wrist = hand.Landmarks[(int)HandLandmark.WRIST];
            foreach(Vector3 v in hand.Landmarks)
            {
                Vector3 nv = v - wrist;
                inputTensor[0, p++] = nv.x;
                inputTensor[0, p++] = nv.y;
                inputTensor[0, p++] = nv.z;
            }
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("landmarks", inputTensor)
            };

            using (var results = m_Session.Run(inputs))
            {
                long label = results.First().AsTensor<long>().ToArray()[0];
                return (HandGestureType)label;
            }
        }
    }
}
