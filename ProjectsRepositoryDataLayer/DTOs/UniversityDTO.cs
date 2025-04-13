using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for University
    /// </summary>
    public class UniversityDTO
    {
        public int UniversityID { get; set; }
        public string UniversityName { get; set; }
        public string SiteLink { get; set; }

        public UniversityDTO(int universityID, string universityName, string siteLink)
        {
            UniversityID = universityID;
            UniversityName = universityName;
            SiteLink = siteLink;
        }
    }
}
