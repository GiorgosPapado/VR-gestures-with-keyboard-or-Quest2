using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Evaluation.Model.Entities;

namespace Assets.Scripts.Evaluation.Actions
{
    public class ShowEntitySystemAction
    {

    }

    public class AddEntityOnMapAction
    {
        public EntityInfo EntityInfo { get; set; }
        public AddEntityOnMapAction(EntityInfo entityInfo)
        {
            this.EntityInfo = entityInfo;
        }
    }
}
