namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsHashtagsPost" />
    /// </summary>
    public class clsHashtagsPost
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
        /// Gets or sets the HashtagsPostID
        /// </summary>
        public int HashtagsPostID { get; set; }

        /// <summary>
        /// Gets or sets the ProjectPostID
        /// </summary>
        public int ProjectPostID { get; set; }

        /// <summary>
        /// Gets or sets the HashtagID
        /// </summary>
        public int HashtagID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsHashtagsPost"/> class.
        /// </summary>
        public clsHashtagsPost()
        {
            this.HashtagsPostID = default;
            this.ProjectPostID = default;
            this.HashtagID = default;
            Mode = enMode.AddNew;
        }

        public HashtagsPostDTO hashtagsPostDTO
        {
            get { return new(this.HashtagsPostID, this.ProjectPostID, this.HashtagID); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsHashtagsPost"/> class from being created.
        /// </summary>
        /// <param name="hashtagsPostDTO">The hashtagsPostDTO<see cref="HashtagsPostDTO"/></param>
        private clsHashtagsPost(HashtagsPostDTO hashtagsPostDTO)
        {
            this.HashtagsPostID = hashtagsPostDTO.HashtagsPostID;
            this.ProjectPostID = hashtagsPostDTO.ProjectPostID;
            this.HashtagID = hashtagsPostDTO.HashtagID;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewHashtagsPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewHashtagsPostAsync()
        {
            this.HashtagsPostID = await clsHashtagsPostData.AddNewHashtagsPostAsync(hashtagsPostDTO);
            return this.HashtagsPostID != 0;
        }

        /// <summary>
        /// The _UpdateHashtagsPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateHashtagsPostAsync()
        {
            return await clsHashtagsPostData.UpdateHashtagsPostAsync(hashtagsPostDTO);
        }

        /// <summary>
        /// The DeleteHashtagsPostAsync
        /// </summary>
        /// <param name="HashtagsPostID">The HashtagsPostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteHashtagsPostAsync(int HashtagsPostID)
        {
            return await clsHashtagsPostData.DeleteHashtagsPostAsync(HashtagsPostID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="HashtagsPostID">The HashtagsPostID<see cref="int"/></param>
        /// <returns>The <see cref="clsHashtagsPost"/></returns>
        public static clsHashtagsPost Find(int HashtagsPostID)
        {
            return new clsHashtagsPost(clsHashtagsPostData.GetHashtagsPostByHashtagsPostID(HashtagsPostID));
        }

        /// <summary>
        /// The IsHashtagsPostExistsAsync
        /// </summary>
        /// <param name="HashtagsPostID">The HashtagsPostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsHashtagsPostExistsAsync(int HashtagsPostID)
        {
            return await clsHashtagsPostData.IsHashtagsPostExistsAsync(HashtagsPostID);
        }

        /// <summary>
        /// The GetAllHashtagsPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{HashtagsPostDTO}}"/></returns>
        public static async Task<List<HashtagsPostDTO>> GetAllHashtagsPostAsync()
        {
            return await clsHashtagsPostData.GetAllHashtagsPostAsync();
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
                    if (await _AddNewHashtagsPostAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateHashtagsPostAsync();
            }

            return false;
        }
    }
}