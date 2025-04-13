namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="clsPersonData" />
    /// </summary>
    public class clsPersonData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsPersonData"/> class.
        /// </summary>
        static clsPersonData()
        {
        }

        private static PersonDTO _MapReaderToPersonDTO(SqlDataReader reader)
        {
            try
            {
                return new PersonDTO(
                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                    reader.GetString(reader.GetOrdinal("FirstName")),
                    reader.IsDBNull(reader.GetOrdinal("SecondName")) ? null : reader.GetString(reader.GetOrdinal("SecondName")),
                    reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? null : reader.GetString(reader.GetOrdinal("ThirdName")),
                    reader.GetString(reader.GetOrdinal("LastName")),
                    reader.GetInt32(reader.GetOrdinal("UniversityID")),
                    reader.IsDBNull(reader.GetOrdinal("ContactEmail")) ? null : reader.GetString(reader.GetOrdinal("ContactEmail")),
                    reader.IsDBNull(reader.GetOrdinal("IsEmployee")) ? (bool?)null : reader.GetBoolean(reader.GetOrdinal("IsEmployee")),
                    reader.IsDBNull(reader.GetOrdinal("CreatedAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
                    reader.IsDBNull(reader.GetOrdinal("Gendor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Gendor"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Gets a person by their ID
        /// </summary>
        /// <param name="PersonID">The person ID</param>
        /// <returns>The person DTO if found, otherwise null</returns>
        public static PersonDTO GetPersonByPersonID(int PersonID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Person_GetPersonByPersonID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        cmd.Parameters.AddWithValue("@PersonID", PersonID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToPersonDTO(reader);
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
        /// Adds a new person
        /// </summary>
        /// <param name="personDTO">The person DTO</param>
        /// <returns>The new person ID if successful, otherwise 0</returns>
        public static async Task<int> AddNewPersonAsync(PersonDTO personDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Person_InsertNewPerson", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@FirstName", personDTO.FirstName);
                        cmd.Parameters.AddWithValue("@SecondName", (object)personDTO.SecondName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ThirdName", (object)personDTO.ThirdName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@LastName", personDTO.LastName);
                        cmd.Parameters.AddWithValue("@UniversityID", personDTO.UniversityID);
                        cmd.Parameters.AddWithValue("@ContactEmail", (object)personDTO.ContactEmail ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsEmployee", (object)personDTO.IsEmployee ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedAt", (object)personDTO.CreatedAt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UpdatedAt", (object)personDTO.UpdatedAt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gendor", (object)personDTO.Gendor ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        return (int)cmd.Parameters["@NewPersonID"].Value;
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
        /// Updates an existing person
        /// </summary>
        /// <param name="personDTO">The person DTO</param>
        /// <returns>True if update was successful, otherwise false</returns>
        public static async Task<bool> UpdatePersonAsync(PersonDTO personDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Person_UpdatePerson", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@PersonID", personDTO.PersonID);
                        cmd.Parameters.AddWithValue("@FirstName", personDTO.FirstName);
                        cmd.Parameters.AddWithValue("@SecondName", (object)personDTO.SecondName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ThirdName", (object)personDTO.ThirdName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@LastName", personDTO.LastName);
                        cmd.Parameters.AddWithValue("@UniversityID", personDTO.UniversityID);
                        cmd.Parameters.AddWithValue("@ContactEmail", (object)personDTO.ContactEmail ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsEmployee", (object)personDTO.IsEmployee ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedAt", (object)personDTO.CreatedAt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UpdatedAt", (object)personDTO.UpdatedAt ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gendor", (object)personDTO.Gendor ?? DBNull.Value);

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
        /// Deletes a person
        /// </summary>
        /// <param name="PersonID">The person ID</param>
        /// <returns>True if deletion was successful, otherwise false</returns>
        public static async Task<bool> DeletePersonAsync(int PersonID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Person_DeletePerson", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@PersonID", PersonID);
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
        /// Checks if a person exists
        /// </summary>
        /// <param name="PersonID">The person ID</param>
        /// <returns>True if exists, otherwise false</returns>
        public static async Task<bool> IsPersonExistsAsync(int PersonID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Person_CheckPersonExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        cmd.Parameters.AddWithValue("@PersonID", PersonID);

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
        /// Gets all persons
        /// </summary>
        /// <returns>A list of person DTOs</returns>
        public static async Task<List<PersonDTO>> GetAllPersonAsync()
        {
            var persons = new List<PersonDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Person_GetAllPerson", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                persons.Add(_MapReaderToPersonDTO(reader));
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

            return persons;
        }
    }

}