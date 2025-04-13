namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsHashtagsPostData" />
    /// </summary>
    public class clsHashtagsPostData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsHashtagsPostData"/> class.
        /// </summary>
        static clsHashtagsPostData()
        {
        }

        private static HashtagsPostDTO _MapReaderToHashtagsPostDTO(SqlDataReader reader)
        {
            try
            {
                return new HashtagsPostDTO(
                    reader.GetInt32(reader.GetOrdinal("HashtagsPostID")),
                    reader.GetInt32(reader.GetOrdinal("ProjectPostID")),
                    reader.GetInt32(reader.GetOrdinal("HashtagID"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The GetHashtagsPostByHashtagsPostID
        /// </summary>
        /// <param name="HashtagsPostID">The HashtagsPostID<see cref="int"/></param>
        /// <returns>The <see cref="HashtagsPostDTO"/></returns>
        public static HashtagsPostDTO GetHashtagsPostByHashtagsPostID(int HashtagsPostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("HashtagsPost_GetHashtagsPostByHashtagsPostID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        cmd.Parameters.AddWithValue("@HashtagsPostID", HashtagsPostID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToHashtagsPostDTO(reader);
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
        /// The AddNewHashtagsPostAsync
        /// </summary>
        /// <param name="hashtagsPostDTO">The hashtagsPostDTO<see cref="HashtagsPostDTO"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public static async Task<int> AddNewHashtagsPostAsync(HashtagsPostDTO hashtagsPostDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("HashtagsPost_InsertNewHashtagsPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@HashtagsPostID", hashtagsPostDTO.HashtagsPostID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", hashtagsPostDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@HashtagID", hashtagsPostDTO.HashtagID);

                        SqlParameter outputIdParam = new SqlParameter("@NewHashtagsPostID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        int newHashtagsPostID = (int)cmd.Parameters["@NewHashtagsPostID"].Value;
                        return newHashtagsPostID;
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
        /// The UpdateHashtagsPostAsync
        /// </summary>
        /// <param name="hashtagsPostDTO">The hashtagsPostDTO<see cref="HashtagsPostDTO"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> UpdateHashtagsPostAsync(HashtagsPostDTO hashtagsPostDTO)
        {
            bool IsRowsAffected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("HashtagsPost_UpdateHashtagsPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@HashtagsPostID", hashtagsPostDTO.HashtagsPostID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", hashtagsPostDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@HashtagID", hashtagsPostDTO.HashtagID);

                        IsRowsAffected = (await cmd.ExecuteNonQueryAsync() > 0);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                IsRowsAffected = false;
            }
            return IsRowsAffected;
        }

        /// <summary>
        /// The DeleteHashtagsPostAsync
        /// </summary>
        /// <param name="HashtagsPostID">The HashtagsPostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteHashtagsPostAsync(int HashtagsPostID)
        {
            bool IsRowsAffected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("HashtagsPost_DeleteHashtagsPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@HashtagsPostID", HashtagsPostID);
                        IsRowsAffected = (await cmd.ExecuteNonQueryAsync() > 0);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                IsRowsAffected = false;
            }
            return IsRowsAffected;
        }

        /// <summary>
        /// The IsHashtagsPostExistsAsync
        /// </summary>
        /// <param name="HashtagsPostID">The HashtagsPostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsHashtagsPostExistsAsync(int HashtagsPostID)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("HashtagsPost_CheckHashtagsPostExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@HashtagsPostID", HashtagsPostID);

                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };

                        cmd.Parameters.Add(returnParameter);
                        await cmd.ExecuteNonQueryAsync();

                        IsFound = (int)returnParameter.Value == 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                IsFound = false;
            }
            return IsFound;
        }

        /// <summary>
        /// The GetAllHashtagsPostAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{HashtagsPostDTO}}"/></returns>
        public static async Task<List<HashtagsPostDTO>> GetAllHashtagsPostAsync()
        {
            var hashtagsPostList = new List<HashtagsPostDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("HashtagsPost_GetAllHashtagsPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var hashtagsPost = _MapReaderToHashtagsPostDTO(reader);
                                if (hashtagsPost != null)
                                {
                                    hashtagsPostList.Add(hashtagsPost);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                hashtagsPostList = null;
            }
            return hashtagsPostList;
        }
    }
}