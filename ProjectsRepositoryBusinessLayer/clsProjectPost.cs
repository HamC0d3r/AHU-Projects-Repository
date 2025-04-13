namespace ProjectsRepositoryDB_Business
{
    using ProjectsRepositoryDB_DataAccess;
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsProjectPost" />
    /// </summary>
    public class clsProjectPost
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
        /// Gets or sets the ProjectPostID
        /// </summary>
        public int ProjectPostID { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the ImagePostID
        /// </summary>
        public int? ImagePostID { get; set; }

        /// <summary>
        /// Gets or sets the UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the LinkID
        /// </summary>
        public int? LinkID { get; set; }

        /// <summary>
        /// Gets or sets the TypePostID
        /// </summary>
        public int? TypePostID { get; set; }

        /// <summary>
        /// Gets or sets the CommentsNum
        /// </summary>
        public int? CommentsNum { get; set; }

        /// <summary>
        /// Gets or sets the LikesNum
        /// </summary>
        public int? LikesNum { get; set; }

        /// <summary>
        /// Gets or sets the ContributorsNum
        /// </summary>
        public int? ContributorsNum { get; set; }

        /// <summary>
        /// Gets or sets the CreatedAt
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedAt
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="clsProjectPost"/> class.
        /// </summary>
        public clsProjectPost()
        {
            this.ProjectPostID = default;
            this.Title = default;
            this.Body = default;
            this.ImagePostID = default;
            this.UserID = default;
            this.LinkID = default;
            this.TypePostID = default;
            this.CommentsNum = default;
            this.LikesNum = default;
            this.ContributorsNum = default;
            this.CreatedAt = default;
            this.UpdatedAt = default;
            Mode = enMode.AddNew;
        }

        public ProjectPostDTO projectPostDTO
        {
            get
            {
                return new(this.ProjectPostID, this.Title, this.Body, this.ImagePostID, this.UserID,
                            this.LinkID, this.TypePostID, this.CommentsNum, this.LikesNum, this.ContributorsNum,
                            this.CreatedAt, this.UpdatedAt);
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="clsProjectPost"/> class from being created.
        /// </summary>
        /// <param name="projectPostDTO">The projectPostDTO<see cref="ProjectPostDTO"/></param>
        private clsProjectPost(ProjectPostDTO projectPostDTO)
        {
            this.ProjectPostID = projectPostDTO.ProjectPostID;
            this.Title = projectPostDTO.Title;
            this.Body = projectPostDTO.Body;
            this.ImagePostID = projectPostDTO.ImagePostID;
            this.UserID = projectPostDTO.UserID;
            this.LinkID = projectPostDTO.LinkID;
            this.TypePostID = projectPostDTO.TypePostID;
            this.CommentsNum = projectPostDTO.CommentsNum;
            this.LikesNum = projectPostDTO.LikesNum;
            this.ContributorsNum = projectPostDTO.ContributorsNum;
            this.CreatedAt = projectPostDTO.CreatedAt;
            this.UpdatedAt = projectPostDTO.UpdatedAt;
            Mode = enMode.Update;
        }

        /// <summary>
        /// The _AddNewProjectPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _AddNewProjectPostAsync()
        {
            this.ProjectPostID = await clsProjectPostData.AddNewProjectPostAsync(projectPostDTO);
            return this.ProjectPostID != 0;
        }

        /// <summary>
        /// The _UpdateProjectPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        private async Task<bool> _UpdateProjectPostAsync()
        {
            return await clsProjectPostData.UpdateProjectPostAsync(projectPostDTO);
        }

        /// <summary>
        /// The DeleteProjectPostAsync
        /// </summary>
        /// <param name="ProjectPostID">The ProjectPostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteProjectPostAsync(int ProjectPostID)
        {
            return await clsProjectPostData.DeleteProjectPostAsync(ProjectPostID);
        }

        /// <summary>
        /// The Find
        /// </summary>
        /// <param name="ProjectPostID">The ProjectPostID<see cref="int"/></param>
        /// <returns>The <see cref="clsProjectPost"/></returns>
        public static clsProjectPost Find(int ProjectPostID)
        {
            return new clsProjectPost(clsProjectPostData.GetProjectPostByProjectPostID(ProjectPostID));
        }

        /// <summary>
        /// The IsProjectPostExistsAsync
        /// </summary>
        /// <param name="ProjectPostID">The ProjectPostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsProjectPostExistsAsync(int ProjectPostID)
        {
            return await clsProjectPostData.IsProjectPostExistsAsync(ProjectPostID);
        }

        /// <summary>
        /// The GetAllProjectPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{ProjectPostDTO}}"/></returns>
        public static async Task<List<ProjectPostDTO>> GetAllProjectPostAsync()
        {
            return await clsProjectPostData.GetAllProjectPostAsync();
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
                    if (await _AddNewProjectPostAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return await _UpdateProjectPostAsync();
            }

            return false;
        }
    }
}