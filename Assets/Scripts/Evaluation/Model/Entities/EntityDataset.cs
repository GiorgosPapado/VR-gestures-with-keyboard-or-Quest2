using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Model.Entities
{
    public static class EntityDataset
    {
        public static IEnumerable<EntityInfo> CreateEntityDataset()
        {
            List<EntityInfo> fsinfo = new List<EntityInfo>
            {
                new EntityInfo
                {
                    Name = "video-#1",
                    EntityType = EntityType.Video,
                    ReferenceEntityID = 0
                },
                new EntityInfo
                {
                    Name = "video-#2",
                    EntityType = EntityType.Video,
                    ReferenceEntityID = 1
                },

                new EntityInfo
                {
                    Name = "tweet-#1",
                    EntityType = EntityType.TwitterTweet,
                    ReferenceEntityID = 0
                },

                new EntityInfo
                {
                    Name = "tweet-#2",
                    EntityType = EntityType.TwitterTweet,
                    ReferenceEntityID = 1
                },
                new EntityInfo
                {
                    Name = "tweet-#3",
                    EntityType = EntityType.TwitterTweet,
                    ReferenceEntityID = 2
                },
                new EntityInfo
                {
                    Name = "fb-photo-#1",
                    EntityType = EntityType.FacebookPhoto,
                    ReferenceEntityID = 0
                },
                new EntityInfo
                {
                    Name = "fb-photo-#2",
                    EntityType = EntityType.FacebookPhoto,
                    ReferenceEntityID = 1
                },
                new EntityInfo
                {
                    Name = "fb-photo-#3",
                    EntityType = EntityType.FacebookPhoto,
                    ReferenceEntityID = 2
                },
                new EntityInfo
                {
                    Name = "fb-photo-#4",
                    EntityType = EntityType.FacebookPhoto,
                    ReferenceEntityID = 3
                }
            };
            return fsinfo;
        }
    }
}
