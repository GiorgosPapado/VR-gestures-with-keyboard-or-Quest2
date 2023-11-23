using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Interactables
{
    public interface IInteractable
    {
        void OnInvokeInteraction();
        void OnHoverEnter();
        void OnHoverExit();
    }
}
