namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsTypePost" />
    /// </summary>
    public class clsTypePost
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
        /// Gets or sets the TypePostID
        /// </summary>
        public int TypePostID { get; set; }

        /// <summary>
        /// Gets or sets the TypePostName
        /// </summary>
        public string TypePostName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsTypePost"/> class.
        /// </summary>
        public clsTypePost()
        {
            this.TypePostID = default;
            this.TypePostName = default;
            Mode = enMode.AddNew;
        }

        public TypePostDTO typePostDTO
        {
            get { return new(this.TypePostID, this.TypePostName); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsTypePost"/> class from being created.
        /// </summary>
        /// <param name="typePostDTO">The typePostDTO<see cref="TypePostDTO"/></param>
        private clsTypePost(TypePostDTO typePostDTO)
        {
            this.TypePostID = typePostDTO.TypePostID;
            this.TypePostName = typePostDTO.TypePostName;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewTypePostAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewTypePostAsync()
        {
            this.TypePostID = await clsTypePostData.AddNewTypePostAsync(typePostDTO);
            return this.TypePostID != 0;
        }

        /// <summary>
        /// The _UpdateTypePostAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateTypePostAsync()
        {
            return await clsTypePostData.UpdateTypePostAsync(typePostDTO);
        }

        /// <summary>
        /// The DeleteTypePostAsync
        /// </summary>
        /// <param name="TypePostID">The TypePostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteTypePostAsync(int TypePostID)
        {
            return await clsTypePostData.DeleteTypePostAsync(TypePostID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="TypePostID">The TypePostID<see cref="int"/></param>
        /// <returns>The <see cref="clsTypePost"/></returns>
        public static clsTypePost Find(int TypePostID)
        {
            return new clsTypePost(clsTypePostData.GetTypePostByTypePostID(TypePostID));
        }

        /// <summary>
        /// The IsTypePostExistsAsync
        /// </summary>
        /// <param name="TypePostID">The TypePostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsTypePostExistsAsync(int TypePostID)
        {
            return await clsTypePostData.IsTypePostExistsAsync(TypePostID);
        }

        /// <summary>
        /// The GetAllTypePostAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{TypePostDTO}}"/></returns>
        public static async Task<List<TypePostDTO>> GetAllTypePostAsync()
        {
            return await clsTypePostData.GetAllTypePostAsync();
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
                    if (await _AddNewTypePostAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateTypePostAsync();
            }

            return false;
        }
    }
}