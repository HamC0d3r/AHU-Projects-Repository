using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess.DTOs
{
    public class LoginUserResponseDTO
    {

        public int UserID { get; set; }

        public string UserName { get; set; }

        public string? Email { get; set; }

        public string ProfileImagePath { get; set; }

        public int? SpecialtyID { get; set; }

        public int Points { get; set; }

        public int PersonID { get; set; }

        public bool isLogin {  get; set; }

        public LoginUserResponseDTO(int UserID, string UserName,
                        string Email, string ProfileImagePath,
                        int? SpecialtyID, int Points, int PersonID)
        {
            this.UserID = UserID;

            this.UserName = UserName;

            this.Email = Email;

            this.ProfileImagePath = ProfileImagePath;

            this.SpecialtyID = SpecialtyID;

            this.Points = Points;

            this.PersonID = PersonID;
        }
    }
}
