using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for PostLink
    /// </summary>
    public class PostLinkDTO
    {
        public int LinkID { get; set; }
        public string Link { get; set; }

        public PostLinkDTO(int linkID, string link)
        {
            LinkID = linkID;
            Link = link;
        }
    }
}
