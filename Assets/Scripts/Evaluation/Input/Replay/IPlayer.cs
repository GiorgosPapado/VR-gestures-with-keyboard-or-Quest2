using Assets.Scripts.Evaluation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
namespace Assets.Scripts.Evaluation.Input.Replay
{
    public interface IPlayer : IActivatable
    {
        IObservable<Unit> OnPlaybackFinished { get; }
    }
}
