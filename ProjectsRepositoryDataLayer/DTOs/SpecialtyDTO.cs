using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for Specialty
    /// </summary>
    public class SpecialtyDTO
    {
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }

        public SpecialtyDTO(int specialtyID, string specialtyName)
        {
            SpecialtyID = specialtyID;
            SpecialtyName = specialtyName;
        }
    }
}
