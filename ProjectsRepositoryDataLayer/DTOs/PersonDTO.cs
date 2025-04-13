using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    /// <summary>
    /// Data Transfer Object for Person
    /// </summary>
    public class PersonDTO
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public int UniversityID { get; set; }
        public string ContactEmail { get; set; }
        public bool? IsEmployee { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Gendor { get; set; }

        public PersonDTO(int personID, string firstName, string secondName, string thirdName,
                        string lastName, int universityID, string contactEmail, bool? isEmployee,
                        DateTime? createdAt, DateTime? updatedAt, int? gendor)
        {
            PersonID = personID;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            UniversityID = universityID;
            ContactEmail = contactEmail;
            IsEmployee = isEmployee;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Gendor = gendor;
        }
    }
}
