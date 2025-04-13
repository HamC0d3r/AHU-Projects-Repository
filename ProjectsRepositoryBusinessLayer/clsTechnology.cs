namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsTechnology" />
    /// </summary>
    public class clsTechnology
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
        /// Gets or sets the TechnologyID
        /// </summary>
        public int TechnologyID { get; set; }

        /// <summary>
        /// Gets or sets the TechnologyName
        /// </summary>
        public string TechnologyName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsTechnology"/> class.
        /// </summary>
        public clsTechnology()
        {
            this.TechnologyID = default;
            this.TechnologyName = default;
            Mode = enMode.AddNew;
        }

        public TechnologyDTO technologyDTO
        {
            get { return new(this.TechnologyID, this.TechnologyName); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsTechnology"/> class from being created.
        /// </summary>
        /// <param name="technologyDTO">The technologyDTO<see cref="TechnologyDTO"/></param>
        private clsTechnology(TechnologyDTO technologyDTO)
        {
            this.TechnologyID = technologyDTO.TechnologyID;
            this.TechnologyName = technologyDTO.TechnologyName;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewTechnologyAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewTechnologyAsync()
        {
            this.TechnologyID = await clsTechnologyData.AddNewTechnologyAsync(technologyDTO);
            return this.TechnologyID != 0;
        }

        /// <summary>
        /// The _UpdateTechnologyAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateTechnologyAsync()
        {
            return await clsTechnologyData.UpdateTechnologyAsync(technologyDTO);
        }

        /// <summary>
        /// The DeleteTechnologyAsync
        /// </summary>
        /// <param name="TechnologyID">The TechnologyID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteTechnologyAsync(int TechnologyID)
        {
            return await clsTechnologyData.DeleteTechnologyAsync(TechnologyID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="TechnologyID">The TechnologyID<see cref="int"/></param>
        /// <returns>The <see cref="clsTechnology"/></returns>
        public static clsTechnology Find(int TechnologyID)
        {
            return new clsTechnology(clsTechnologyData.GetTechnologyByTechnologyID(TechnologyID));
        }

        /// <summary>
        /// The IsTechnologyExistsAsync
        /// </summary>
        /// <param name="TechnologyID">The TechnologyID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsTechnologyExistsAsync(int TechnologyID)
        {
            return await clsTechnologyData.IsTechnologyExistsAsync(TechnologyID);
        }

        /// <summary>
        /// The GetAllTechnologiesAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{TechnologyDTO}}"/></returns>
        public static async Task<List<TechnologyDTO>> GetAllTechnologiesAsync()
        {
            return await clsTechnologyData.GetAllTechnologiesAsync();
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
                    if (await _AddNewTechnologyAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateTechnologyAsync();
            }

            return false;
        }
    }
}