namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsTechPostData" />
    /// </summary>
    public class clsTechPostData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsTechPostData"/> class.
        /// </summary>
        static clsTechPostData()
        {
        }

        private static TechPostDTO _MapReaderToTechPostDTO(SqlDataReader reader)
        {
            try
            {
                return new TechPostDTO(
                    reader.GetInt32(reader.GetOrdinal("TechPostID")),
                    reader.GetInt32(reader.GetOrdinal("ProjectPostID")),
                    reader.GetInt32(reader.GetOrdinal("TechnologyID"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a tech post by its ID
        /// </summary>
        /// <param name="TechPostID">The tech post ID</param>
        /// <returns>The tech post DTO if found, otherwise null</returns>
        public static TechPostDTO GetTechPostByTechPostID(int TechPostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TechPost_GetTechPostByTechPostID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@TechPostID", TechPostID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToTechPostDTO(reader);
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
        /// Adds a new tech post
        /// </summary>
        /// <param name="techPostDTO">The tech post DTO</param>
        /// <returns>The new tech post ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewTechPostAsync(TechPostDTO techPostDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TechPost_InsertNewTechPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@TechPostID", techPostDTO.TechPostID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", techPostDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@TechnologyID", techPostDTO.TechnologyID);

                        SqlParameter outputIdParam = new SqlParameter("@NewTechPostID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewTechPostID"].Value;
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
        /// Updates an existing tech post
        /// </summary>
        /// <param name="techPostDTO">The tech post DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdateTechPostAsync(TechPostDTO techPostDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TechPost_UpdateTechPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@TechPostID", techPostDTO.TechPostID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", techPostDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@TechnologyID", techPostDTO.TechnologyID);

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
        /// Deletes a tech post
        /// </summary>
        /// <param name="TechPostID">The tech post ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeleteTechPostAsync(int TechPostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TechPost_DeleteTechPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@TechPostID", TechPostID);
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
        /// Checks if a tech post exists
        /// </summary>
        /// <param name="TechPostID">The tech post ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsTechPostExistsAsync(int TechPostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TechPost_CheckTechPostExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@TechPostID", TechPostID);

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
        /// Gets all tech posts
        /// </summary>
        /// <returns>A list of tech post DTOs</returns>
        public static async Task<List<TechPostDTO>> GetAllTechPostAsync()
        {
            var techPosts = new List<TechPostDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("TechPost_GetAllTechPost", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                techPosts.Add(_MapReaderToTechPostDTO(reader));
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

            return techPosts;
        }
    }


}