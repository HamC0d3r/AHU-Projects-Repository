namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsSpecialtyData" />
    /// </summary>
    public class clsSpecialtyData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsSpecialtyData"/> class.
        /// </summary>
        static clsSpecialtyData()
        {
        }

        private static SpecialtyDTO _MapReaderToSpecialtyDTO(SqlDataReader reader)
        {
            try
            {
                return new SpecialtyDTO(
                    reader.GetInt32(reader.GetOrdinal("SpecialtyID")),
                    reader.GetString(reader.GetOrdinal("SpecialtyName"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a specialty by its ID
        /// </summary>
        /// <param name="SpecialtyID">The specialty ID</param>
        /// <returns>The specialty DTO if found, otherwise null</returns>
        public static SpecialtyDTO GetSpecialtyBySpecialtyID(int SpecialtyID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Specialty_GetSpecialtyBySpecialtyID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@SpecialtyID", SpecialtyID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToSpecialtyDTO(reader);
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
        /// Adds a new specialty
        /// </summary>
        /// <param name="specialtyDTO">The specialty DTO</param>
        /// <returns>The new specialty ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewSpecialtyAsync(SpecialtyDTO specialtyDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Specialty_InsertNewSpecialty", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@SpecialtyID", specialtyDTO.SpecialtyID);
                        cmd.Parameters.AddWithValue("@SpecialtyName", specialtyDTO.SpecialtyName);

                        SqlParameter outputIdParam = new SqlParameter("@NewSpecialtyID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewSpecialtyID"].Value;
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
        /// Updates an existing specialty
        /// </summary>
        /// <param name="specialtyDTO">The specialty DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdateSpecialtyAsync(SpecialtyDTO specialtyDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Specialty_UpdateSpecialty", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@SpecialtyID", specialtyDTO.SpecialtyID);
                        cmd.Parameters.AddWithValue("@SpecialtyName", specialtyDTO.SpecialtyName);

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
        /// Deletes a specialty
        /// </summary>
        /// <param name="SpecialtyID">The specialty ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeleteSpecialtyAsync(int SpecialtyID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Specialty_DeleteSpecialty", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@SpecialtyID", SpecialtyID);
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
        /// Checks if a specialty exists
        /// </summary>
        /// <param name="SpecialtyID">The specialty ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsSpecialtyExistsAsync(int SpecialtyID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Specialty_CheckSpecialtyExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@SpecialtyID", SpecialtyID);

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
        /// Gets all specialties
        /// </summary>
        /// <returns>A list of specialty DTOs</returns>
        public static async Task<List<SpecialtyDTO>> GetAllSpecialtyAsync()
        {
            var specialties = new List<SpecialtyDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Specialty_GetAllSpecialty", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                specialties.Add(_MapReaderToSpecialtyDTO(reader));
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

            return specialties;
        }
    }


}