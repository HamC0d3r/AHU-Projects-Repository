using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for TechPost
    /// </summary>
    public class TechPostDTO
    {
        public int TechPostID { get; set; }
        public int ProjectPostID { get; set; }
        public int TechnologyID { get; set; }

        public TechPostDTO(int techPostID, int projectPostID, int technologyID)
        {
            TechPostID = techPostID;
            ProjectPostID = projectPostID;
            TechnologyID = technologyID;
        }
    }
}
