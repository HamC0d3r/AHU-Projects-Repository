namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsUniversityData" />
    /// </summary>
    public class clsUniversityData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsUniversityData"/> class.
        /// </summary>
        static clsUniversityData()
        {
        }

        private static UniversityDTO _MapReaderToUniversityDTO(SqlDataReader reader)
        {
            try
            {
                return new UniversityDTO(
                    reader.GetInt32(reader.GetOrdinal("UniversityID")),
                    reader.GetString(reader.GetOrdinal("UniversityName")),
                    reader.IsDBNull(reader.GetOrdinal("SiteLink")) ? null : reader.GetString(reader.GetOrdinal("SiteLink"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a university by its ID
        /// </summary>
        /// <param name="UniversityID">The university ID</param>
        /// <returns>The university DTO if found, otherwise null</returns>
        public static UniversityDTO GetUniversityByUniversityID(int UniversityID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("University_GetUniversityByUniversityID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@UniversityID", UniversityID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToUniversityDTO(reader);
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
        /// Adds a new university
        /// </summary>
        /// <param name="universityDTO">The university DTO</param>
        /// <returns>The new university ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewUniversityAsync(UniversityDTO universityDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("University_InsertNewUniversity", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@UniversityID", universityDTO.UniversityID);
                        cmd.Parameters.AddWithValue("@UniversityName", universityDTO.UniversityName);
                        cmd.Parameters.AddWithValue("@SiteLink", (object)universityDTO.SiteLink ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewUniversityID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewUniversityID"].Value;
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
        /// Updates an existing university
        /// </summary>
        /// <param name="universityDTO">The university DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdateUniversityAsync(UniversityDTO universityDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("University_UpdateUniversity", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@UniversityID", universityDTO.UniversityID);
                        cmd.Parameters.AddWithValue("@UniversityName", universityDTO.UniversityName);
                        cmd.Parameters.AddWithValue("@SiteLink", (object)universityDTO.SiteLink ?? DBNull.Value);

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
        /// Deletes a university
        /// </summary>
        /// <param name="UniversityID">The university ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeleteUniversityAsync(int UniversityID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("University_DeleteUniversity", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@UniversityID", UniversityID);
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
        /// Checks if a university exists
        /// </summary>
        /// <param name="UniversityID">The university ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsUniversityExistsAsync(int UniversityID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("University_CheckUniversityExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@UniversityID", UniversityID);

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
        /// Gets all universities
        /// </summary>
        /// <returns>A list of university DTOs</returns>
        public static async Task<List<UniversityDTO>> GetAllUniversityAsync()
        {
            var universities = new List<UniversityDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("University_GetAllUniversity", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                universities.Add(_MapReaderToUniversityDTO(reader));
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

            return universities;
        }
    }


}