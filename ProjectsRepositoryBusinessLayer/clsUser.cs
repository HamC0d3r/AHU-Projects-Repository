namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using ProjectsRepositoryDB_DataAccess.DTOs;
    using System;
    using System.Data;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsUser" />
    /// </summary>
    public class clsUser
    {
        /// <summary>
        /// Defines the enMode
        /// </summary>
        public enum enMode
        {/// <summary>
         /// Defines the AddNew
         /// </summary>
            AddNew,
            /// <summary>
            /// Defines the Update
            /// </summary>
            Update
        }

        /// <summary>
        /// Gets or sets the Mode
        /// </summary>
        public enMode Mode { get; set; } = enMode.AddNew;

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="clsUser"/> class.
        /// </summary>
        public clsUser()
        {
            this.UserID = default;

            this.UserName = default;

            this.Email = default;

            this.PasswordHash = default;

            this.ProfileImagePath = default;

            this.SpecialtyID = default;

            this.Points = default;

            this.CreatedAt = default;

            this.UpdatedAt = default;

            this.PersonID = default;

            Mode = enMode.AddNew;
        }
        public UserDTO userDTO { get { return new (this.UserID, this.UserName,
                                                this.Email, this.PasswordHash, this.ProfileImagePath,
                                                this.SpecialtyID, this.Points, this.CreatedAt,
                                                this.UpdatedAt, this.PersonID); } }
        /// <summary>
        /// Prevents a default instance of the <see cref="clsUser"/> class from being created.
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="UserName">The UserName<see cref="string"/></param>
        /// <param name="Email">The Email<see cref="string"/></param>
        /// <param name="PasswordHash">The PasswordHash<see cref="string"/></param>
        /// <param name="ProfileImagePath">The ProfileImagePath<see cref="string"/></param>
        /// <param name="SpecialtyID">The SpecialtyID<see cref="int?"/></param>
        /// <param name="Points">The Points<see cref="int?"/></param>
        /// <param name="CreatedAt">The CreatedAt<see cref="DateTime?"/></param>
        /// <param name="UpdatedAt">The UpdatedAt<see cref="DateTime?"/></param>
        /// <param name="PersonID">The PersonID<see cref="int?"/></param>
        private clsUser(int UserID, string UserName,
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

            Mode = enMode.Update;
        }
        private clsUser(UserDTO userDTO , enMode mode = enMode.AddNew) 
        {
            this.UserID = userDTO.UserID;

            this.UserName = userDTO.UserName;

            this.Email = userDTO.Email;

            this.PasswordHash = userDTO.PasswordHash;

            this.ProfileImagePath = userDTO.ProfileImagePath;

            this.SpecialtyID = userDTO.SpecialtyID;

            this.Points = userDTO.Points;

            this.CreatedAt = userDTO.CreatedAt;

            this.UpdatedAt = userDTO.UpdatedAt;

            this.PersonID = userDTO.PersonID;
            
            Mode = mode;
        }
        /// <summary>
        /// The _AddNewUserAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewUserAsync()
        {
            this.UserID = await (clsUserData.AddNewUserAsync(userDTO));
            return UserID != 0;
        }

        /// <summary>
        /// The _UpdateUserAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateUserAsync()
        {
            return await clsUserData.UpdateUserAsync(userDTO);
        }

        /// <summary>
        /// The DeleteUserAsync
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteUserAsync(int UserID)
        {
            return await clsUserData.DeleteUserAsync(UserID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="clsUser"/></returns>
        public static clsUser Find(int UserID)
        {
            UserDTO userDTO = clsUserData.GetUserByUserID(UserID);

            if (userDTO != null)
            {
                return new clsUser(userDTO, enMode.Update);
            }
            else
                return null;

        }

        /// <summary>
        /// The IsUserExistsAsync
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsUserExistsAsync(int UserID)
        {

            return await clsUserData.IsUserExistsAsync(UserID);
        }

        /// <summary>
        /// The GetAllUsersAsync
        /// </summary>
        /// <returns>The <see cref="Task{DataTable}"/></returns>
        public static async Task<List<UserDTO>> GetAllUsersAsync()
        {

            return await clsUserData.GetAllUsersAsync();
        }

        /// <summary>
        /// The Save
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public async Task<bool> Save()
        {

            switch (Mode)
            {

                case enMode.AddNew:
                    if (await _AddNewUserAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateUserAsync();

            }

            return false;
        }

        public static LoginUserResponseDTO Login(LoginRequestDTO loginRequest) 
        {
            LoginUserResponseDTO loginUserResponseDTO = clsUserData.GetUserByEmailAndPassword(loginRequest);
            if(loginUserResponseDTO != null) 
            {
                return loginUserResponseDTO;
            }
            return null;
        }
    }
}
