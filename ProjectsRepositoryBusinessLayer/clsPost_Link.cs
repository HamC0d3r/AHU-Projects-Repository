namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsPost_Link" />
    /// </summary>
    public class clsPost_Link
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
        /// Gets or sets the LinkID
        /// </summary>
        public int LinkID { get; set; }

        /// <summary>
        /// Gets or sets the Link
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsPost_Link"/> class.
        /// </summary>
        public clsPost_Link()
        {
            this.LinkID = default;
            this.Link = default;
            Mode = enMode.AddNew;
        }

        public PostLinkDTO postLinkDTO
        {
            get { return new(this.LinkID, this.Link); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsPost_Link"/> class from being created.
        /// </summary>
        /// <param name="postLinkDTO">The postLinkDTO<see cref="PostLinkDTO"/></param>
        private clsPost_Link(PostLinkDTO postLinkDTO)
        {
            this.LinkID = postLinkDTO.LinkID;
            this.Link = postLinkDTO.Link;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewPost_LinkAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewPost_LinkAsync()
        {
            this.LinkID = await clsPost_LinkData.AddNewPost_LinkAsync(postLinkDTO);
            return this.LinkID != 0;
        }

        /// <summary>
        /// The _UpdatePost_LinkAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdatePost_LinkAsync()
        {
            return await clsPost_LinkData.UpdatePost_LinkAsync(postLinkDTO);
        }

        /// <summary>
        /// The DeletePost_LinkAsync
        /// </summary>
        /// <param name="LinkID">The LinkID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeletePost_LinkAsync(int LinkID)
        {
            return await clsPost_LinkData.DeletePost_LinkAsync(LinkID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="LinkID">The LinkID<see cref="int"/></param>
        /// <returns>The <see cref="clsPost_Link"/></returns>
        public static clsPost_Link Find(int LinkID)
        {
            return new clsPost_Link(clsPost_LinkData.GetPost_LinkByLinkID(LinkID));
        }

        /// <summary>
        /// The IsPost_LinkExistsAsync
        /// </summary>
        /// <param name="LinkID">The LinkID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsPost_LinkExistsAsync(int LinkID)
        {
            return await clsPost_LinkData.IsPost_LinkExistsAsync(LinkID);
        }

        /// <summary>
        /// The GetAllPost_LinksAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{PostLinkDTO}}"/></returns>
        public static async Task<List<PostLinkDTO>> GetAllPost_LinksAsync()
        {
            return await clsPost_LinkData.GetAllPost_LinksAsync();
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
                    if (await _AddNewPost_LinkAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdatePost_LinkAsync();
            }

            return false;
        }
    }
}