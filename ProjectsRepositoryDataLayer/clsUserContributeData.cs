using ProjectsRepositoryDB_DataAccess;

namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsUserContributeData" />
    /// </summary>
    public class clsUserContributeData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsUserContributeData"/> class.
        /// </summary>
        static clsUserContributeData()
        {
        }

        private static UserContributeDTO _MapReaderToUserContributeDTO(SqlDataReader reader)
        {
            try
            {
                return new UserContributeDTO(
                    reader.GetInt32(reader.GetOrdinal("ContributeID")),
                    reader.GetInt32(reader.GetOrdinal("UserID")),
                    reader.GetInt32(reader.GetOrdinal("ProjectPostID")),
                    reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a user contribute by its ID
        /// </summary>
        /// <param name="ContributeID">The contribute ID</param>
        /// <returns>The user contribute DTO if found, otherwise null</returns>
        public static UserContributeDTO GetUserContributeByContributeID(int ContributeID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserContributes_GetUserContributeByContributeID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@ContributeID", ContributeID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToUserContributeDTO(reader);
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
        /// Adds a new user contribute
        /// </summary>
        /// <param name="userContributeDTO">The user contribute DTO</param>
        /// <returns>The new contribute ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewUserContributeAsync(UserContributeDTO userContributeDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserContributes_InsertNewUserContribute", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@ContributeID", userContributeDTO.ContributeID);
                        cmd.Parameters.AddWithValue("@UserID", userContributeDTO.UserID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", userContributeDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@Description", (object)userContributeDTO.Description ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewContributeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewContributeID"].Value;
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
        /// Updates an existing user contribute
        /// </summary>
        /// <param name="userContributeDTO">The user contribute DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdateUserContributeAsync(UserContributeDTO userContributeDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserContributes_UpdateUserContribute", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@ContributeID", userContributeDTO.ContributeID);
                        cmd.Parameters.AddWithValue("@UserID", userContributeDTO.UserID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", userContributeDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@Description", (object)userContributeDTO.Description ?? DBNull.Value);

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
        /// Deletes a user contribute
        /// </summary>
        /// <param name="ContributeID">The contribute ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeleteUserContributeAsync(int ContributeID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserContributes_DeleteUserContribute", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@ContributeID", ContributeID);
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
        /// Checks if a user contribute exists
        /// </summary>
        /// <param name="ContributeID">The contribute ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsUserContributeExistsAsync(int ContributeID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserContributes_CheckUserContributeExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@ContributeID", ContributeID);

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
        /// Gets all user contributes
        /// </summary>
        /// <returns>A list of user contribute DTOs</returns>
        public static async Task<List<UserContributeDTO>> GetAllUserContributesAsync()
        {
            var userContributes = new List<UserContributeDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UserContributes_GetAllUserContributes", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                userContributes.Add(_MapReaderToUserContributeDTO(reader));
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

            return userContributes;
        }
    }

   
}