using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Input.Replay
{
    public interface IStateCapturer
    {
        void CaptureState();
        void FlushState(string filePath, bool threaded);   
        int BufferSize { get; }
    }
}
