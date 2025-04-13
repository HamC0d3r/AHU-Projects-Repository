namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsComment" />
    /// </summary>
    public class clsComment
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
        /// Gets or sets the CommentID
        /// </summary>
        public int CommentID { get; set; }

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
        /// Gets or sets the ImagePath
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsComment"/> class.
        /// </summary>
        public clsComment()
        {
            this.CommentID = default;
            this.ProjectPostID = default;
            this.UserID = default;
            this.Date = default;
            this.ImagePath = default;
            Mode = enMode.AddNew;
        }

        public CommentDTO commentDTO
        {
            get { return new(this.CommentID, this.ProjectPostID, this.UserID, this.Date, this.ImagePath); }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsComment"/> class from being created.
        /// </summary>
        /// <param name="commentDTO">The commentDTO<see cref="CommentDTO"/></param>
        private clsComment(CommentDTO commentDTO)
        {
            this.CommentID = commentDTO.CommentID;
            this.ProjectPostID = commentDTO.ProjectPostID;
            this.UserID = commentDTO.UserID;
            this.Date = commentDTO.Date;
            this.ImagePath = commentDTO.ImagePath;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewCommentAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewCommentAsync()
        {
            this.CommentID = await clsCommentData.AddNewCommentAsync(commentDTO);
            return this.CommentID != 0;
        }

        /// <summary>
        /// The _UpdateCommentAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateCommentAsync()
        {
            return await clsCommentData.UpdateCommentAsync(commentDTO);
        }

        /// <summary>
        /// The DeleteCommentAsync
        /// </summary>
        /// <param name="CommentID">The CommentID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteCommentAsync(int CommentID)
        {
            return await clsCommentData.DeleteCommentAsync(CommentID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="CommentID">The CommentID<see cref="int"/></param>
        /// <returns>The <see cref="clsComment"/></returns>
        public static clsComment Find(int CommentID)
        {
            return new clsComment(clsCommentData.GetCommentByCommentID(CommentID));
        }

        /// <summary>
        /// The IsCommentExistsAsync
        /// </summary>
        /// <param name="CommentID">The CommentID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsCommentExistsAsync(int CommentID)
        {
            return await clsCommentData.IsCommentExistsAsync(CommentID);
        }

        /// <summary>
        /// The GetAllCommentsAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{CommentDTO}}"/></returns>
        public static async Task<List<CommentDTO>> GetAllCommentsAsync()
        {
            return await clsCommentData.GetAllCommentsAsync();
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
                    if (await _AddNewCommentAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateCommentAsync();
            }

            return false;
        }
    }
}