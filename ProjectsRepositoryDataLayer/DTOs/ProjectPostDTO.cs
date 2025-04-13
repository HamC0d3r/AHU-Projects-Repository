using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for ProjectPost
    /// </summary>
    public class ProjectPostDTO
    {
        public int ProjectPostID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int? ImagePostID { get; set; }
        public int UserID { get; set; }
        public int? LinkID { get; set; }
        public int? TypePostID { get; set; }
        public int? CommentsNum { get; set; }
        public int? LikesNum { get; set; }
        public int? ContributorsNum { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ProjectPostDTO(int projectPostID, string title, string body, int? imagePostID,
                            int userID, int? linkID, int? typePostID, int? commentsNum,
                            int? likesNum, int? contributorsNum, DateTime? createdAt, DateTime? updatedAt)
        {
            ProjectPostID = projectPostID;
            Title = title;
            Body = body;
            ImagePostID = imagePostID;
            UserID = userID;
            LinkID = linkID;
            TypePostID = typePostID;
            CommentsNum = commentsNum;
            LikesNum = likesNum;
            ContributorsNum = contributorsNum;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
