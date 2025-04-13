using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRepositoryDB_DataAccess
{
    public class UserDTO
    {
        public UserDTO(int UserID, string UserName,
                        string Email, string PasswordHash, string ProfileImagePath,
                        int? SpecialtyID, int? Points, DateTime? CreatedAt,
                        DateTime? UpdatedAt, int? PersonID)
        {
            this.UserID = UserID;

            this.UserName = UserName;

            this.Email = Email;

            this.PasswordHash = PasswordHash;

            this.ProfileImagePath = ProfileImagePath;

            this.SpecialtyID = SpecialtyID;

            this.Points = Points;

            this.CreatedAt = CreatedAt;

            this.UpdatedAt = UpdatedAt;

            this.PersonID = PersonID;

        }
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the PasswordHash
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the ProfileImagePath
        /// </summary>
        public string ProfileImagePath { get; set; }

        /// <summary>
        /// Gets or sets the SpecialtyID
        /// </summary>
        public int? SpecialtyID { get; set; }

        /// <summary>
        /// Gets or sets the Points
        /// </summary>
        public int? Points { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedAt
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the PersonID
        /// </summary>
        public int? PersonID { get; set; }
    }
}
