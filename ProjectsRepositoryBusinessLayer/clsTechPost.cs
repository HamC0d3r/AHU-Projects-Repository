namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsTechPost" />
    /// </summary>
    public class clsTechPost
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
        /// Gets or sets the TechPostID
        /// </summary>
        public int TechPostID { get; set; }

        /// <summary>
        /// Gets or sets the ProjectPostID
        /// </summary>
        public int ProjectPostID { get; set; }

        /// <summary>
        /// Gets or sets the TechnologyID
        /// </summary>
        public int TechnologyID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsTechPost"/> class.
        /// </summary>
        public clsTechPost()
        {
            this.TechPostID = default;
            this.ProjectPostID = default;
            this.TechnologyID = default;
            Mode = enMode.AddNew;
        }

        public TechPostDTO techPostDTO
        {
            get { return new(this.TechPostID, this.ProjectPostID, this.TechnologyID); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsTechPost"/> class from being created.
        /// </summary>
        /// <param name="techPostDTO">The techPostDTO<see cref="TechPostDTO"/></param>
        private clsTechPost(TechPostDTO techPostDTO)
        {
            this.TechPostID = techPostDTO.TechPostID;
            this.ProjectPostID = techPostDTO.ProjectPostID;
            this.TechnologyID = techPostDTO.TechnologyID;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewTechPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewTechPostAsync()
        {
            this.TechPostID = await clsTechPostData.AddNewTechPostAsync(techPostDTO);
            return this.TechPostID != 0;
        }

        /// <summary>
        /// The _UpdateTechPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateTechPostAsync()
        {
            return await clsTechPostData.UpdateTechPostAsync(techPostDTO);
        }

        /// <summary>
        /// The DeleteTechPostAsync
        /// </summary>
        /// <param name="TechPostID">The TechPostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteTechPostAsync(int TechPostID)
        {
            return await clsTechPostData.DeleteTechPostAsync(TechPostID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="TechPostID">The TechPostID<see cref="int"/></param>
        /// <returns>The <see cref="clsTechPost"/></returns>
        public static clsTechPost Find(int TechPostID)
        {
            return new clsTechPost(clsTechPostData.GetTechPostByTechPostID(TechPostID));
        }

        /// <summary>
        /// The IsTechPostExistsAsync
        /// </summary>
        /// <param name="TechPostID">The TechPostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsTechPostExistsAsync(int TechPostID)
        {
            return await clsTechPostData.IsTechPostExistsAsync(TechPostID);
        }

        /// <summary>
        /// The GetAllTechPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{TechPostDTO}}"/></returns>
        public static async Task<List<TechPostDTO>> GetAllTechPostAsync()
        {
            return await clsTechPostData.GetAllTechPostAsync();
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
                    if (await _AddNewTechPostAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateTechPostAsync();
            }

            return false;
        }
    }
}