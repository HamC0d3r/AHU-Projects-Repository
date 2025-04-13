namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsUniversity" />
    /// </summary>
    public class clsUniversity
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
        /// Gets or sets the UniversityID
        /// </summary>
        public int UniversityID { get; set; }

        /// <summary>
        /// Gets or sets the UniversityName
        /// </summary>
        public string UniversityName { get; set; }

        /// <summary>
        /// Gets or sets the SiteLink
        /// </summary>
        public string SiteLink { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsUniversity"/> class.
        /// </summary>
        public clsUniversity()
        {
            this.UniversityID = default;
            this.UniversityName = default;
            this.SiteLink = default;
            Mode = enMode.AddNew;
        }

        public UniversityDTO universityDTO
        {
            get { return new(this.UniversityID, this.UniversityName, this.SiteLink); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsUniversity"/> class from being created.
        /// </summary>
        /// <param name="universityDTO">The universityDTO<see cref="UniversityDTO"/></param>
        private clsUniversity(UniversityDTO universityDTO)
        {
            this.UniversityID = universityDTO.UniversityID;
            this.UniversityName = universityDTO.UniversityName;
            this.SiteLink = universityDTO.SiteLink;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewUniversityAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewUniversityAsync()
        {
            this.UniversityID = await clsUniversityData.AddNewUniversityAsync(universityDTO);
            return this.UniversityID != 0;
        }

        /// <summary>
        /// The _UpdateUniversityAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateUniversityAsync()
        {
            return await clsUniversityData.UpdateUniversityAsync(universityDTO);
        }

        /// <summary>
        /// The DeleteUniversityAsync
        /// </summary>
        /// <param name="UniversityID">The UniversityID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteUniversityAsync(int UniversityID)
        {
            return await clsUniversityData.DeleteUniversityAsync(UniversityID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="UniversityID">The UniversityID<see cref="int"/></param>
        /// <returns>The <see cref="clsUniversity"/></returns>
        public static clsUniversity Find(int UniversityID)
        {
            return new clsUniversity(clsUniversityData.GetUniversityByUniversityID(UniversityID));
        }

        /// <summary>
        /// The IsUniversityExistsAsync
        /// </summary>
        /// <param name="UniversityID">The UniversityID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsUniversityExistsAsync(int UniversityID)
        {
            return await clsUniversityData.IsUniversityExistsAsync(UniversityID);
        }

        /// <summary>
        /// The GetAllUniversityAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{UniversityDTO}}"/></returns>
        public static async Task<List<UniversityDTO>> GetAllUniversityAsync()
        {
            return await clsUniversityData.GetAllUniversityAsync();
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
                    if (await _AddNewUniversityAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateUniversityAsync();
            }

            return false;
        }
    }
}