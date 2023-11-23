using Assets.Scripts.Evaluation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Scripts.Evaluation.Input.Replay
{
    public class NullPlayer : Activatable, IPlayer
    {
        public IObservable<Unit> OnPlaybackFinished => Observable.Empty<Unit>();

        protected override void OnDisable()
        {
           
        }

        protected override void OnEnable()
        {
            
        }
    }
}
