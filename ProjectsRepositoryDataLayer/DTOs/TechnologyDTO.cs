using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for Technology
    /// </summary>
    public class TechnologyDTO
    {
        public int TechnologyID { get; set; }
        public string TechnologyName { get; set; }

        public TechnologyDTO(int technologyID, string technologyName)
        {
            TechnologyID = technologyID;
            TechnologyName = technologyName;
        }
    }
}
