using Assets.Scripts.Evaluation.Gestures.ONNX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Gestures.Interface
{
    public interface IVRControllerGestureProvider
    {
        VRControllerGestureType GetLatestVRControllerGesture();
        IObservable<VRControllerGestureType> VRControllerGestureStream { get; }        
    }
}
