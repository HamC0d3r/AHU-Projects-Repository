namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsHashtag" />
    /// </summary>
    public class clsHashtag
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
        /// Gets or sets the HashtagID
        /// </summary>
        public int HashtagID { get; set; }

        /// <summary>
        /// Gets or sets the HashtagName
        /// </summary>
        public string HashtagName { get; set; }

        /// <summary>
        /// Gets or sets the PostsCount
        /// </summary>
        public int? PostsCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsHashtag"/> class.
        /// </summary>
        public clsHashtag()
        {
            this.HashtagID = default;
            this.HashtagName = default;
            this.PostsCount = default;
            Mode = enMode.AddNew;
        }

        public HashtagDTO hashtagDTO
        {
            get { return new(this.HashtagID, this.HashtagName, this.PostsCount); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsHashtag"/> class from being created.
        /// </summary>
        /// <param name="hashtagDTO">The hashtagDTO<see cref="HashtagDTO"/></param>
        private clsHashtag(HashtagDTO hashtagDTO)
        {
            this.HashtagID = hashtagDTO.HashtagID;
            this.HashtagName = hashtagDTO.HashtagName;
            this.PostsCount = hashtagDTO.PostsCount;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewHashtagAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewHashtagAsync()
        {
            this.HashtagID = await clsHashtagData.AddNewHashtagAsync(hashtagDTO);
            return this.HashtagID != 0;
        }

        /// <summary>
        /// The _UpdateHashtagAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateHashtagAsync()
        {
            return await clsHashtagData.UpdateHashtagAsync(hashtagDTO);
        }

        /// <summary>
        /// The DeleteHashtagAsync
        /// </summary>
        /// <param name="HashtagID">The HashtagID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteHashtagAsync(int HashtagID)
        {
            return await clsHashtagData.DeleteHashtagAsync(HashtagID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="HashtagID">The HashtagID<see cref="int"/></param>
        /// <returns>The <see cref="clsHashtag"/></returns>
        public static clsHashtag Find(int HashtagID)
        {
            return new clsHashtag(clsHashtagData.GetHashtagByHashtagID(HashtagID));
        }

        /// <summary>
        /// The IsHashtagExistsAsync
        /// </summary>
        /// <param name="HashtagID">The HashtagID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsHashtagExistsAsync(int HashtagID)
        {
            return await clsHashtagData.IsHashtagExistsAsync(HashtagID);
        }

        /// <summary>
        /// The GetAllHashtagsAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{HashtagDTO}}"/></returns>
        public static async Task<List<HashtagDTO>> GetAllHashtagsAsync()
        {
            return await clsHashtagData.GetAllHashtagsAsync();
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
                    if (await _AddNewHashtagAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateHashtagAsync();
            }

            return false;
        }
    }
}