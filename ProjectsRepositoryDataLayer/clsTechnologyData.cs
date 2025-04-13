namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsTechnologyData" />
    /// </summary>
    public class clsTechnologyData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsTechnologyData"/> class.
        /// </summary>
        static clsTechnologyData()
        {
        }

        private static TechnologyDTO _MapReaderToTechnologyDTO(SqlDataReader reader)
        {
            try
            {
                return new TechnologyDTO(
                    reader.GetInt32(reader.GetOrdinal("TechnologyID")),
                    reader.GetString(reader.GetOrdinal("TechnologyName"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a technology by its ID
        /// </summary>
        /// <param name="TechnologyID">The technology ID</param>
        /// <returns>The technology DTO if found, otherwise null</returns>
        public static TechnologyDTO GetTechnologyByTechnologyID(int TechnologyID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Technologies_GetTechnologyByTechnologyID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@TechnologyID", TechnologyID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToTechnologyDTO(reader);
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
        /// Adds a new technology
        /// </summary>
        /// <param name="technologyDTO">The technology DTO</param>
        /// <returns>The new technology ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewTechnologyAsync(TechnologyDTO technologyDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Technologies_InsertNewTechnology", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@TechnologyID", technologyDTO.TechnologyID);
                        cmd.Parameters.AddWithValue("@TechnologyName", technologyDTO.TechnologyName);

                        SqlParameter outputIdParam = new SqlParameter("@NewTechnologyID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewTechnologyID"].Value;
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
        /// Updates an existing technology
        /// </summary>
        /// <param name="technologyDTO">The technology DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdateTechnologyAsync(TechnologyDTO technologyDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Technologies_UpdateTechnology", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@TechnologyID", technologyDTO.TechnologyID);
                        cmd.Parameters.AddWithValue("@TechnologyName", technologyDTO.TechnologyName);

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
        /// Deletes a technology
        /// </summary>
        /// <param name="TechnologyID">The technology ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeleteTechnologyAsync(int TechnologyID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Technologies_DeleteTechnology", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@TechnologyID", TechnologyID);
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
        /// Checks if a technology exists
        /// </summary>
        /// <param name="TechnologyID">The technology ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsTechnologyExistsAsync(int TechnologyID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Technologies_CheckTechnologyExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@TechnologyID", TechnologyID);

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
        /// Gets all technologies
        /// </summary>
        /// <returns>A list of technology DTOs</returns>
        public static async Task<List<TechnologyDTO>> GetAllTechnologiesAsync()
        {
            var technologies = new List<TechnologyDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Technologies_GetAllTechnologies", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                technologies.Add(_MapReaderToTechnologyDTO(reader));
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

            return technologies;
        }
    }


}