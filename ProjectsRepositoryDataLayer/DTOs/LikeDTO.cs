using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    public class LikeDTO
    {
        public int LikeID { get; set; }
        public int ProjectPostID { get; set; }
        public int UserID { get; set; }
        public DateTime? Date { get; set; }
        public string TypeOfLike { get; set; }

        public LikeDTO(int likeID, int projectPostID, int userID, DateTime? date, string typeOfLike)
        {
            LikeID = likeID;
            ProjectPostID = projectPostID;
            UserID = userID;
            Date = date;
            TypeOfLike = typeOfLike;
        }
    }
}
