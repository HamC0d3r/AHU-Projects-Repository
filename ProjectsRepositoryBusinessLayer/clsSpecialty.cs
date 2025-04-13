namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsSpecialty" />
    /// </summary>
    public class clsSpecialty
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
        /// Gets or sets the SpecialtyID
        /// </summary>
        public int SpecialtyID { get; set; }

        /// <summary>
        /// Gets or sets the SpecialtyName
        /// </summary>
        public string SpecialtyName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsSpecialty"/> class.
        /// </summary>
        public clsSpecialty()
        {
            this.SpecialtyID = default;
            this.SpecialtyName = default;
            Mode = enMode.AddNew;
        }

        public SpecialtyDTO specialtyDTO
        {
            get { return new(this.SpecialtyID, this.SpecialtyName); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsSpecialty"/> class from being created.
        /// </summary>
        /// <param name="specialtyDTO">The specialtyDTO<see cref="SpecialtyDTO"/></param>
        private clsSpecialty(SpecialtyDTO specialtyDTO)
        {
            this.SpecialtyID = specialtyDTO.SpecialtyID;
            this.SpecialtyName = specialtyDTO.SpecialtyName;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewSpecialtyAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewSpecialtyAsync()
        {
            this.SpecialtyID = await clsSpecialtyData.AddNewSpecialtyAsync(specialtyDTO);
            return this.SpecialtyID != 0;
        }

        /// <summary>
        /// The _UpdateSpecialtyAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateSpecialtyAsync()
        {
            return await clsSpecialtyData.UpdateSpecialtyAsync(specialtyDTO);
        }

        /// <summary>
        /// The DeleteSpecialtyAsync
        /// </summary>
        /// <param name="SpecialtyID">The SpecialtyID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteSpecialtyAsync(int SpecialtyID)
        {
            return await clsSpecialtyData.DeleteSpecialtyAsync(SpecialtyID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="SpecialtyID">The SpecialtyID<see cref="int"/></param>
        /// <returns>The <see cref="clsSpecialty"/></returns>
        public static clsSpecialty Find(int SpecialtyID)
        {
            return new clsSpecialty(clsSpecialtyData.GetSpecialtyBySpecialtyID(SpecialtyID));
        }

        /// <summary>
        /// The IsSpecialtyExistsAsync
        /// </summary>
        /// <param name="SpecialtyID">The SpecialtyID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsSpecialtyExistsAsync(int SpecialtyID)
        {
            return await clsSpecialtyData.IsSpecialtyExistsAsync(SpecialtyID);
        }

        /// <summary>
        /// The GetAllSpecialtyAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{SpecialtyDTO}}"/></returns>
        public static async Task<List<SpecialtyDTO>> GetAllSpecialtyAsync()
        {
            return await clsSpecialtyData.GetAllSpecialtyAsync();
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
                    if (await _AddNewSpecialtyAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateSpecialtyAsync();
            }

            return false;
        }
    }
}