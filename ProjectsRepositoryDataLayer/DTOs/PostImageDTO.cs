using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    public class PostImageDTO
    {
        public int ImagePostID { get; set; }
        public string ImagePath { get; set; }

        public PostImageDTO(int imagePostID, string imagePath)
        {
            ImagePostID = imagePostID;
            ImagePath = imagePath;
        }
    }
}
