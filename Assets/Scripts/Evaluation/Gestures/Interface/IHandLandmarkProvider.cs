﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Scripts.Evaluation.Gestures.Interface
{
    public interface IHandLandmarkProvider
    {
        IObservable<HandData> HandLandmarkStream { get; }
        HandData GetLatestHandLandmarks();
    }
}
