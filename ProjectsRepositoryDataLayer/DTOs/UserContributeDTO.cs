
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for UserContribute
    /// </summary>
    public class UserContributeDTO
    {
        public int ContributeID { get; set; }
        public int UserID { get; set; }
        public int ProjectPostID { get; set; }
        public string Description { get; set; }

        public UserContributeDTO(int contributeID, int userID, int projectPostID, string description)
        {
            ContributeID = contributeID;
            UserID = userID;
            ProjectPostID = projectPostID;
            Description = description;
        }
    }
}
