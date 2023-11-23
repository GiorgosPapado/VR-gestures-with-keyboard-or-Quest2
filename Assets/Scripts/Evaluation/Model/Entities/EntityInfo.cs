using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Model.Entities
{
    public enum EntityType
    {
        Video,
        TwitterTweet,
        FacebookPhoto        
    }
    public class EntityInfo : IRepositoryItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public EntityType EntityType { get; set; }
        public int ReferenceEntityID { get; set; }
    }
}
