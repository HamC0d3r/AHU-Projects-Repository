namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsPost_LinkData" />
    /// </summary>
    public class clsPost_LinkData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsPost_LinkData"/> class.
        /// </summary>
        static clsPost_LinkData()
        {
        }

        private static PostLinkDTO _MapReaderToPostLinkDTO(SqlDataReader reader)
        {
            try
            {
                return new PostLinkDTO(
                    reader.GetInt32(reader.GetOrdinal("LinkID")),
                    reader.GetString(reader.GetOrdinal("Link"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a post link by its ID
        /// </summary>
        /// <param name="LinkID">The link ID</param>
        /// <returns>The post link DTO if found, otherwise null</returns>
        public static PostLinkDTO GetPost_LinkByLinkID(int LinkID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Links_GetPost_LinkByLinkID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@LinkID", LinkID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToPostLinkDTO(reader);
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
        /// Adds a new post link
        /// </summary>
        /// <param name="postLinkDTO">The post link DTO</param>
        /// <returns>The new link ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewPost_LinkAsync(PostLinkDTO postLinkDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Links_InsertNewPost_Link", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@LinkID", postLinkDTO.LinkID);
                        cmd.Parameters.AddWithValue("@Link", postLinkDTO.Link);

                        SqlParameter outputIdParam = new SqlParameter("@NewLinkID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewLinkID"].Value;
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
        /// Updates an existing post link
        /// </summary>
        /// <param name="postLinkDTO">The post link DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdatePost_LinkAsync(PostLinkDTO postLinkDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Links_UpdatePost_Link", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@LinkID", postLinkDTO.LinkID);
                        cmd.Parameters.AddWithValue("@Link", postLinkDTO.Link);

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
        /// Deletes a post link
        /// </summary>
        /// <param name="LinkID">The link ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeletePost_LinkAsync(int LinkID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Links_DeletePost_Link", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@LinkID", LinkID);
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
        /// Checks if a post link exists
        /// </summary>
        /// <param name="LinkID">The link ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsPost_LinkExistsAsync(int LinkID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Links_CheckPost_LinkExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@LinkID", LinkID);

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
        /// Gets all post links
        /// </summary>
        /// <returns>A list of post link DTOs</returns>
        public static async Task<List<PostLinkDTO>> GetAllPost_LinksAsync()
        {
            var postLinks = new List<PostLinkDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Links_GetAllPost_Links", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                postLinks.Add(_MapReaderToPostLinkDTO(reader));
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

            return postLinks;
        }
    }


}