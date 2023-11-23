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
using Assets.Scripts.Evaluation.Model;

namespace Assets.Scripts.Evaluation.Gestures.ONNX
{
    public enum VRControllerGestureType
    {
        I,
        U,
        //V,        
        INFINITY,
        LessThan,
        None
    }
    public class VRControllerGestures
    {
        private InferenceSession m_Session;
        public Handedness Handedness { get; set; } = Handedness.RIGHT;

        public VRControllerGestures()
        {
            m_Session = new InferenceSession(".\\gestures\\vr-controller-gestrec.onnx");
        }
        public VRControllerGestureType Inference(VRGestureSample gesture)
        {
            var gr = Handedness == Handedness.RIGHT ? gesture.RightControllerData : gesture.LeftControllerData;
            //Tensor<float> inputTensor = new DenseTensor<float>(new int[] { 1, 7, gr.Count });
            Tensor<float> inputTensor = new DenseTensor<float>(new int[] { 1, 3, gr.Count });
            Vector3 ref_pos = gr[0].Position;
            Quaternion inv_ref_orient = Quaternion.Inverse(gr[0].Orientation);
            for (int i=0; i<gr.Count;i++)
            {
                int feat_index = 0;
                Vector3 trans_pos = inv_ref_orient * (gr[i].Position - ref_pos);
                Quaternion trans_orient = inv_ref_orient * gr[i].Orientation;
                inputTensor[0, feat_index++, i] = trans_pos.x;
                inputTensor[0, feat_index++, i] = trans_pos.y;
                inputTensor[0, feat_index++, i] = trans_pos.z;
                //inputTensor[0, feat_index++, i] = trans_orient.x;
                //inputTensor[0, feat_index++, i] = trans_orient.y;
                //inputTensor[0, feat_index++, i] = trans_orient.z;
                //inputTensor[0, feat_index++, i] = trans_orient.w;
            }

            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input", inputTensor)
            };

            using (var results = m_Session.Run(inputs))
            {
                var q = results.First().AsTensor<float>().ToArray();                
                var (conf, label) = results.First().AsTensor<float>().ToArray().Select((val, index) => (val, index)).Max();
                //Debug.Log($"Class: {(VRControllerGestureType)label}/{conf}");
                return (VRControllerGestureType)label;
            }

        }
    }
}
