using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Assets.Scripts.Evaluation.Model.Facebook
{
    public static class FBDataset
    {
        public static IEnumerable<FbInfo> CreateFbDataset()
        {
            List<FbInfo> dataset = new List<FbInfo>
            {
                new FbInfo
                {
                    Username = "User1",
                    Hashtags = { "#tag1", "#tag2", "#tag3" },
                    Date = DateTime.Parse("01/17/2021 17:30:00",CultureInfo.InvariantCulture),
                    IP = "10.73.1.20",
                    Post = "Lorem ipsum dolor sit amet, consectetur adipiscing elit"
                },
                new FbInfo
                {
                    Username = "User2",
                    Hashtags = { "#tag1","#tag3"},
                    Date = DateTime.Parse("01/17/2021 17:35:00",CultureInfo.InvariantCulture),
                    IP = "10.21.71.111",
                    Post = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur"
                },
                new FbInfo
                {
                    Username = "User3",
                    Hashtags = { "#tag4","#tag5"},
                    Date = DateTime.Parse("01/17/2021 17:39:00",CultureInfo.InvariantCulture),
                    IP = "101.201.17.211",
                    Post = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium"
                },
                new FbInfo
                {
                    Username = "User4",
                    Hashtags = { "#tag1","#tag5","#tag3"},
                    Date = DateTime.Parse("01/17/2021 17:41:00",CultureInfo.InvariantCulture),
                    IP = "171.111.11.200",
                    Post = "Aut odit aut fugit, sed quia consequuntur magni dolores"
                }

            };
            return dataset;
        }
    }
}
