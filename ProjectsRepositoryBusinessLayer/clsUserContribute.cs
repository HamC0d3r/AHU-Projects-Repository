namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsUserContribute" />
    /// </summary>
    public class clsUserContribute
    {
        /// <summary>
        /// Defines the enMode
        /// </summary>
        public enum enMode
        {
            /// <summary>
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
        /// Gets or sets the ContributeID
        /// </summary>
        public int ContributeID { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the ProjectPostID
        /// </summary>
        public int ProjectPostID { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsUserContribute"/> class.
        /// </summary>
        public clsUserContribute()
        {
            this.ContributeID = default;
            this.UserID = default;
            this.ProjectPostID = default;
            this.Description = default;
            Mode = enMode.AddNew;
        }

        public UserContributeDTO userContributeDTO
        {
            get { return new(this.ContributeID, this.UserID, this.ProjectPostID, this.Description); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsUserContribute"/> class from being created.
        /// </summary>
        /// <param name="userContributeDTO">The userContributeDTO<see cref="UserContributeDTO"/></param>
        private clsUserContribute(UserContributeDTO userContributeDTO)
        {
            this.ContributeID = userContributeDTO.ContributeID;
            this.UserID = userContributeDTO.UserID;
            this.ProjectPostID = userContributeDTO.ProjectPostID;
            this.Description = userContributeDTO.Description;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewUserContributeAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewUserContributeAsync()
        {
            this.ContributeID = await clsUserContributeData.AddNewUserContributeAsync(userContributeDTO);
            return this.ContributeID != 0;
        }

        /// <summary>
        /// The _UpdateUserContributeAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateUserContributeAsync()
        {
            return await clsUserContributeData.UpdateUserContributeAsync(userContributeDTO);
        }

        /// <summary>
        /// The DeleteUserContributeAsync
        /// </summary>
        /// <param name="ContributeID">The ContributeID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteUserContributeAsync(int ContributeID)
        {
            return await clsUserContributeData.DeleteUserContributeAsync(ContributeID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="ContributeID">The ContributeID<see cref="int"/></param>
        /// <returns>The <see cref="clsUserContribute"/></returns>
        public static clsUserContribute Find(int ContributeID)
        {
            return new clsUserContribute(clsUserContributeData.GetUserContributeByContributeID(ContributeID));
        }

        /// <summary>
        /// The IsUserContributeExistsAsync
        /// </summary>
        /// <param name="ContributeID">The ContributeID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsUserContributeExistsAsync(int ContributeID)
        {
            return await clsUserContributeData.IsUserContributeExistsAsync(ContributeID);
        }

        /// <summary>
        /// The GetAllUserContributesAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{UserContributeDTO}}"/></returns>
        public static async Task<List<UserContributeDTO>> GetAllUserContributesAsync()
        {
            return await clsUserContributeData.GetAllUserContributesAsync();
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
                    if (await _AddNewUserContributeAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateUserContributeAsync();
            }

            return false;
        }
    }
}