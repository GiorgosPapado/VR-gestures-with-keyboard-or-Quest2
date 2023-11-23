using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Evaluation.Model.Filesystem
{
    public static class FSDataset
    {
        public static IEnumerable<FSInfo> CreateFSDataset()
        {
            List<FSInfo> fsinfo = new List<FSInfo>
            {
                new FSInfo
                {
                    Filename = "video-1.mp4",
                    Filetype = FSFileType.Video,                    
                },
                new FSInfo
                {
                    Filename = "video-2.mp4",
                    Filetype = FSFileType.Video,
                },

                new FSInfo
                {
                    Filename = "image1.jpg",
                    Filetype = FSFileType.Photo
                },

                new FSInfo
                {
                    Filename = "image2.jpg",
                    Filetype = FSFileType.Photo
                },
                new FSInfo
                {
                    Filename = "text.pdf",
                    Filetype = FSFileType.PDF
                },
                new FSInfo
                {
                    Filename = "datasheet.csv",
                    Filetype = FSFileType.CSV
                }
            };
            return fsinfo;
        }
    }
}
