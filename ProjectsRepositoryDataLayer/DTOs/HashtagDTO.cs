using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for Hashtag
    /// </summary>
    public class HashtagDTO
    {
        public int HashtagID { get; set; }
        public string HashtagName { get; set; }
        public int? PostsCount { get; set; }

        public HashtagDTO(int hashtagID, string hashtagName, int? postsCount)
        {
            HashtagID = hashtagID;
            HashtagName = hashtagName;
            PostsCount = postsCount;
        }
    }
}
