using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Utils
{
    public interface IActivatable
    {
        public bool Enabled { get; set; }
    }
}
