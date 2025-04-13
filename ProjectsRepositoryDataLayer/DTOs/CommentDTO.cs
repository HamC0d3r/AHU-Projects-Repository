using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    public class CommentDTO
    {
        public int CommentID { get; set; }
        public int ProjectPostID { get; set; }
        public int UserID { get; set; }
        public DateTime? Date { get; set; }
        public string ImagePath { get; set; }

        public CommentDTO(int commentID, int projectPostID, int userID, DateTime? date, string imagePath)
        {
            CommentID = commentID;
            ProjectPostID = projectPostID;
            UserID = userID;
            Date = date;
            ImagePath = imagePath;
        }
    }
}
