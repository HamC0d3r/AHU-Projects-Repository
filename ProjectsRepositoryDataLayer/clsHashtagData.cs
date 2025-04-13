namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsHashtagData" />
    /// </summary>
    public class clsHashtagData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsHashtagData"/> class.
        /// </summary>
        static clsHashtagData()
        {
        }

        private static HashtagDTO _MapReaderToHashtagDTO(SqlDataReader reader)
        {
            try
            {
                return new HashtagDTO(
                    reader.GetInt32(reader.GetOrdinal("HashtagID")),
                    reader.GetString(reader.GetOrdinal("HashtagName")),
                    reader.IsDBNull(reader.GetOrdinal("PostsCount")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("PostsCount"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a hashtag by its ID
        /// </summary>
        /// <param name="HashtagID">The hashtag ID</param>
        /// <returns>The hashtag DTO if found, otherwise null</returns>
        public static HashtagDTO GetHashtagByHashtagID(int HashtagID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Hashtags_GetHashtagByHashtagID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@HashtagID", HashtagID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToHashtagDTO(reader);
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
        /// Adds a new hashtag
        /// </summary>
        /// <param name="hashtagDTO">The hashtag DTO</param>
        /// <returns>The new hashtag ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewHashtagAsync(HashtagDTO hashtagDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Hashtags_InsertNewHashtag", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@HashtagID", hashtagDTO.HashtagID);
                        cmd.Parameters.AddWithValue("@HashtagName", hashtagDTO.HashtagName);
                        cmd.Parameters.AddWithValue("@PostsCount", (object)hashtagDTO.PostsCount ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewHashtagID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewHashtagID"].Value;
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
        /// Updates an existing hashtag
        /// </summary>
        /// <param name="hashtagDTO">The hashtag DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdateHashtagAsync(HashtagDTO hashtagDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Hashtags_UpdateHashtag", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@HashtagID", hashtagDTO.HashtagID);
                        cmd.Parameters.AddWithValue("@HashtagName", hashtagDTO.HashtagName);
                        cmd.Parameters.AddWithValue("@PostsCount", (object)hashtagDTO.PostsCount ?? DBNull.Value);

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
        /// Deletes a hashtag
        /// </summary>
        /// <param name="HashtagID">The hashtag ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeleteHashtagAsync(int HashtagID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Hashtags_DeleteHashtag", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@HashtagID", HashtagID);
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
        /// Checks if a hashtag exists
        /// </summary>
        /// <param name="HashtagID">The hashtag ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsHashtagExistsAsync(int HashtagID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Hashtags_CheckHashtagExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@HashtagID", HashtagID);

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
        /// Gets all hashtags
        /// </summary>
        /// <returns>A list of hashtag DTOs</returns>
        public static async Task<List<HashtagDTO>> GetAllHashtagsAsync()
        {
            var hashtags = new List<HashtagDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Hashtags_GetAllHashtags", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                hashtags.Add(_MapReaderToHashtagDTO(reader));
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

            return hashtags;
        }
    }


}