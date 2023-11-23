using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Assets.Scripts.Evaluation.Model.Twitter
{
    public static class TwitterDataset
    {
        public static IEnumerable<TweetInfo> CreateTwitterDataset()
        {
            List<TweetInfo> dataset = new List<TweetInfo>
            {
                new TweetInfo
                {
                    Username = "User3",
                    Hashtags = { "#tag1", "#tag2", "#tag3" },
                    Date = DateTime.Parse("01/17/2021 18:30:00",CultureInfo.InvariantCulture),
                    IP = "10.7.6.120",
                    Tweet = "Sed ut perspiciatis unde omnis"
                },
                new TweetInfo
                {
                    Username = "User4",
                    Hashtags = { "#tag4","#tag5"},
                    Date = DateTime.Parse("01/17/2021 18:35:00",CultureInfo.InvariantCulture),
                    IP = "10.21.77.125",
                    Tweet = "Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis"
                },
                new TweetInfo
                {
                    Username = "User5",
                    Hashtags = { "#tag6","#tag7"},
                    Date = DateTime.Parse("01/17/2021 19:39:00",CultureInfo.InvariantCulture),
                    IP = "101.201.17.211",
                    Tweet = "Magni dolores eos qui ratione voluptatem se"
                },

            };
            return dataset;
        }
    }
}
