namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsTypePostData" />
    /// </summary>
    public class clsTypePostData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsTypePostData"/> class.
        /// </summary>
        static clsTypePostData()
        {
        }

        private static TypePostDTO _MapReaderToTypePostDTO(SqlDataReader reader)
        {
            try
            {
                return new TypePostDTO(
                    reader.GetInt32(reader.GetOrdinal("TypePostID")),
                    reader.GetString(reader.GetOrdinal("TypePostName"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a type post by its ID
        /// </summary>
        /// <param name="TypePostID">The type post ID</param>
        /// <returns>The type post DTO if found, otherwise null</returns>
        public static TypePostDTO GetTypePostByTypePostID(int TypePostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TypePost_GetTypePostByTypePostID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@TypePostID", TypePostID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToTypePostDTO(reader);
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
        /// Adds a new type post
        /// </summary>
        /// <param name="typePostDTO">The type post DTO</param>
        /// <returns>The new type post ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewTypePostAsync(TypePostDTO typePostDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TypePost_InsertNewTypePost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@TypePostID", typePostDTO.TypePostID);
                        cmd.Parameters.AddWithValue("@TypePostName", typePostDTO.TypePostName);

                        SqlParameter outputIdParam = new SqlParameter("@NewTypePostID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewTypePostID"].Value;
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
        /// Updates an existing type post
        /// </summary>
        /// <param name="typePostDTO">The type post DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdateTypePostAsync(TypePostDTO typePostDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TypePost_UpdateTypePost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@TypePostID", typePostDTO.TypePostID);
                        cmd.Parameters.AddWithValue("@TypePostName", typePostDTO.TypePostName);

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
        /// Deletes a type post
        /// </summary>
        /// <param name="TypePostID">The type post ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeleteTypePostAsync(int TypePostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TypePost_DeleteTypePost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@TypePostID", TypePostID);
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
        /// Checks if a type post exists
        /// </summary>
        /// <param name="TypePostID">The type post ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsTypePostExistsAsync(int TypePostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TypePost_CheckTypePostExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@TypePostID", TypePostID);

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
        /// Gets all type posts
        /// </summary>
        /// <returns>A list of type post DTOs</returns>
        public static async Task<List<TypePostDTO>> GetAllTypePostAsync()
        {
            var typePosts = new List<TypePostDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TypePost_GetAllTypePost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                typePosts.Add(_MapReaderToTypePostDTO(reader));
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

            return typePosts;
        }
    }


}