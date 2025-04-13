namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsPost_Image" />
    /// </summary>
    public class clsPost_Image
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
        /// Gets or sets the ImagePostID
        /// </summary>
        public int ImagePostID { get; set; }

        /// <summary>
        /// Gets or sets the ImagePath
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsPost_Image"/> class.
        /// </summary>
        public clsPost_Image()
        {
            this.ImagePostID = default;
            this.ImagePath = default;
            Mode = enMode.AddNew;
        }

        public PostImageDTO postImageDTO
        {
            get { return new(this.ImagePostID, this.ImagePath); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsPost_Image"/> class from being created.
        /// </summary>
        /// <param name="postImageDTO">The postImageDTO<see cref="PostImageDTO"/></param>
        private clsPost_Image(PostImageDTO postImageDTO)
        {
            this.ImagePostID = postImageDTO.ImagePostID;
            this.ImagePath = postImageDTO.ImagePath;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewPost_ImageAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewPost_ImageAsync()
        {
            this.ImagePostID = await clsPost_ImageData.AddNewPost_ImageAsync(postImageDTO);
            return this.ImagePostID != 0;
        }

        /// <summary>
        /// The _UpdatePost_ImageAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdatePost_ImageAsync()
        {
            return await clsPost_ImageData.UpdatePost_ImageAsync(postImageDTO);
        }

        /// <summary>
        /// The DeletePost_ImageAsync
        /// </summary>
        /// <param name="ImagePostID">The ImagePostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeletePost_ImageAsync(int ImagePostID)
        {
            return await clsPost_ImageData.DeletePost_ImageAsync(ImagePostID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="ImagePostID">The ImagePostID<see cref="int"/></param>
        /// <returns>The <see cref="clsPost_Image"/></returns>
        public static clsPost_Image Find(int ImagePostID)
        {
            return new clsPost_Image(clsPost_ImageData.GetPost_ImageByImagePostID(ImagePostID));
        }

        /// <summary>
        /// The IsPost_ImageExistsAsync
        /// </summary>
        /// <param name="ImagePostID">The ImagePostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsPost_ImageExistsAsync(int ImagePostID)
        {
            return await clsPost_ImageData.IsPost_ImageExistsAsync(ImagePostID);
        }

        /// <summary>
        /// The GetAllPost_ImageAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{PostImageDTO}}"/></returns>
        public static async Task<List<PostImageDTO>> GetAllPost_ImageAsync()
        {
            return await clsPost_ImageData.GetAllPost_ImageAsync();
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
                    if (await _AddNewPost_ImageAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdatePost_ImageAsync();
            }

            return false;
        }
    }
}