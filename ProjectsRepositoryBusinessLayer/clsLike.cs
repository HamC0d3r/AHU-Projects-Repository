namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsLike" />
    /// </summary>
    public class clsLike
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
        /// Gets or sets the LikeID
        /// </summary>
        public int LikeID { get; set; }

        /// <summary>
        /// Gets or sets the ProjectPostID
        /// </summary>
        public int ProjectPostID { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the Date
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or sets the TypeOfLike
        /// </summary>
        public string TypeOfLike { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsLike"/> class.
        /// </summary>
        public clsLike()
        {
            this.LikeID = default;
            this.ProjectPostID = default;
            this.UserID = default;
            this.Date = default;
            this.TypeOfLike = default;
            Mode = enMode.AddNew;
        }

        public LikeDTO likeDTO
        {
            get { return new(this.LikeID, this.ProjectPostID, this.UserID, this.Date, this.TypeOfLike); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsLike"/> class from being created.
        /// </summary>
        /// <param name="likeDTO">The likeDTO<see cref="LikeDTO"/></param>
        private clsLike(LikeDTO likeDTO)
        {
            this.LikeID = likeDTO.LikeID;
            this.ProjectPostID = likeDTO.ProjectPostID;
            this.UserID = likeDTO.UserID;
            this.Date = likeDTO.Date;
            this.TypeOfLike = likeDTO.TypeOfLike;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewLikeAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewLikeAsync()
        {
            this.LikeID = await clsLikeData.AddNewLikeAsync(likeDTO);
            return this.LikeID != 0;
        }

        /// <summary>
        /// The _UpdateLikeAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateLikeAsync()
        {
            return await clsLikeData.UpdateLikeAsync(likeDTO);
        }

        /// <summary>
        /// The DeleteLikeAsync
        /// </summary>
        /// <param name="LikeID">The LikeID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteLikeAsync(int LikeID)
        {
            return await clsLikeData.DeleteLikeAsync(LikeID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="LikeID">The LikeID<see cref="int"/></param>
        /// <returns>The <see cref="clsLike"/></returns>
        public static clsLike Find(int LikeID)
        {
            return new clsLike(clsLikeData.GetLikeByLikeID(LikeID));
        }

        /// <summary>
        /// The IsLikeExistsAsync
        /// </summary>
        /// <param name="LikeID">The LikeID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsLikeExistsAsync(int LikeID)
        {
            return await clsLikeData.IsLikeExistsAsync(LikeID);
        }

        /// <summary>
        /// The GetAllLikesAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{LikeDTO}}"/></returns>
        public static async Task<List<LikeDTO>> GetAllLikesAsync()
        {
            return await clsLikeData.GetAllLikesAsync();
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
                    if (await _AddNewLikeAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateLikeAsync();
            }

            return false;
        }
    }
}