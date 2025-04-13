namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsProjectPostData" />
    /// </summary>
    public class clsProjectPostData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsProjectPostData"/> class.
        /// </summary>
        static clsProjectPostData()
        {
        }

        private static ProjectPostDTO _MapReaderToProjectPostDTO(SqlDataReader reader)
        {
            try
            {
                return new ProjectPostDTO(
                    reader.GetInt32(reader.GetOrdinal("ProjectPostID")),
                    reader.GetString(reader.GetOrdinal("Title")),
                    reader.GetString(reader.GetOrdinal("Body")),
                    reader.IsDBNull(reader.GetOrdinal("ImagePostID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ImagePostID")),
                    reader.GetInt32(reader.GetOrdinal("UserID")),
                    reader.IsDBNull(reader.GetOrdinal("LinkID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("LinkID")),
                    reader.IsDBNull(reader.GetOrdinal("TypePostID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("TypePostID")),
                    reader.IsDBNull(reader.GetOrdinal("CommentsNum")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("CommentsNum")),
                    reader.IsDBNull(reader.GetOrdinal("LikesNum")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("LikesNum")),
                    reader.IsDBNull(reader.GetOrdinal("ContributorsNum")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ContributorsNum")),
                    reader.IsDBNull(reader.GetOrdinal("CreatedAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a project post by its ID
        /// </summary>
        /// <param name="ProjectPostID">The project post ID</param>
        /// <returns>The project post DTO if found, otherwise null</returns>
        public static ProjectPostDTO GetProjectPostByProjectPostID(int ProjectPostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProjectPost_GetProjectPostByProjectPostID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@ProjectPostID", ProjectPostID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToProjectPostDTO(reader);
                            }
                            return null;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Adds a new project post
        /// </summary>
        /// <param name="projectPostDTO">The project post DTO</param>
        /// <returns>The new project post ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewProjectPostAsync(ProjectPostDTO projectPostDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProjectPost_InsertNewProjectPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@Title", projectPostDTO.Title);
                        cmd.Parameters.AddWithValue("@Body", projectPostDTO.Body);
                        cmd.Parameters.AddWithValue("@ImagePostID", (object)projectPostDTO.ImagePostID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserID", projectPostDTO.UserID);
                        cmd.Parameters.AddWithValue("@LinkID", (object)projectPostDTO.LinkID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TypePostID", (object)projectPostDTO.TypePostID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CommentsNum", (object)projectPostDTO.CommentsNum ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@LikesNum", (object)projectPostDTO.LikesNum ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ContributorsNum", (object)projectPostDTO.ContributorsNum ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedAt", (object)projectPostDTO.CreatedAt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UpdatedAt", (object)projectPostDTO.UpdatedAt ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewProjectPostID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewProjectPostID"].Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Updates an existing project post
        /// </summary>
        /// <param name="projectPostDTO">The project post DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdateProjectPostAsync(ProjectPostDTO projectPostDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProjectPost_UpdateProjectPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@ProjectPostID", projectPostDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@Title", projectPostDTO.Title);
                        cmd.Parameters.AddWithValue("@Body", projectPostDTO.Body);
                        cmd.Parameters.AddWithValue("@ImagePostID", (object)projectPostDTO.ImagePostID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserID", projectPostDTO.UserID);
                        cmd.Parameters.AddWithValue("@LinkID", (object)projectPostDTO.LinkID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TypePostID", (object)projectPostDTO.TypePostID ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CommentsNum", (object)projectPostDTO.CommentsNum ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@LikesNum", (object)projectPostDTO.LikesNum ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ContributorsNum", (object)projectPostDTO.ContributorsNum ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedAt", (object)projectPostDTO.CreatedAt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UpdatedAt", (object)projectPostDTO.UpdatedAt ?? DBNull.Value);

                        return (await cmd.ExecuteNonQueryAsync()) > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes a project post
        /// </summary>
        /// <param name="ProjectPostID">The project post ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeleteProjectPostAsync(int ProjectPostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProjectPost_DeleteProjectPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@ProjectPostID", ProjectPostID);
                        return (await cmd.ExecuteNonQueryAsync()) > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Checks if a project post exists
        /// </summary>
        /// <param name="ProjectPostID">The project post ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsProjectPostExistsAsync(int ProjectPostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProjectPost_CheckProjectPostExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@ProjectPostID", ProjectPostID);

                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        cmd.Parameters.Add(returnParameter);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)returnParameter.Value == 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets all project posts
        /// </summary>
        /// <returns>A list of project post DTOs</returns>
        public static async Task<List<ProjectPostDTO>> GetAllProjectPostAsync()
        {
            var projectPosts = new List<ProjectPostDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("ProjectPost_GetAllProjectPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                projectPosts.Add(_MapReaderToProjectPostDTO(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }

            return projectPosts;
        }
    }


}