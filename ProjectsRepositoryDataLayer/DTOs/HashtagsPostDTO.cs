using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    public class HashtagsPostDTO
    {
        public int HashtagsPostID { get; set; }
        public int ProjectPostID { get; set; }
        public int HashtagID { get; set; }

        public HashtagsPostDTO(int hashtagsPostID, int projectPostID, int hashtagID)
        {
            HashtagsPostID = hashtagsPostID;
            ProjectPostID = projectPostID;
            HashtagID = hashtagID;
        }
    }
}
