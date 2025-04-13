namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using ProjectsRepositoryDB_DataAccess.DTOs;

    /// <summary>
    /// Defines the <see cref="clsUserData" />
    /// </summary>
    public class clsUserData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsUserData"/> class.
        /// </summary>
        static clsUserData()
        {
        }
        private static UserDTO _MapReaderToUserDTO(SqlDataReader reader)
        {
            try
            {
                return new UserDTO(
                    reader.GetInt32(reader.GetOrdinal("UserID")),
                    reader.GetString(reader.GetOrdinal("UserName")),
                    reader.GetString(reader.GetOrdinal("Email")),
                    reader.GetString(reader.GetOrdinal("PasswordHash")),
                    reader.IsDBNull(reader.GetOrdinal("ProfileImagePath")) ? null : reader.GetString(reader.GetOrdinal("ProfileImagePath")),
                    reader.IsDBNull(reader.GetOrdinal("SpecialtyID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SpecialtyID")),
                    reader.IsDBNull(reader.GetOrdinal("Points")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Points")),
                    reader.IsDBNull(reader.GetOrdinal("CreatedAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
                    reader.IsDBNull(reader.GetOrdinal("PersonID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("PersonID"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The GetUserByUserID
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="UserName">The UserName<see cref="string"/></param>
        /// <param name="Email">The Email<see cref="string"/></param>
        /// <param name="PasswordHash">The PasswordHash<see cref="string"/></param>
        /// <param name="ProfileImagePath">The ProfileImagePath<see cref="string"/></param>
        /// <param name="SpecialtyID">The SpecialtyID<see cref="int?"/></param>
        /// <param name="Points">The Points<see cref="int?"/></param>
        /// <param name="CreatedAt">The CreatedAt<see cref="DateTime?"/></param>
        /// <param name="UpdatedAt">The UpdatedAt<see cref="DateTime?"/></param>
        /// <param name="PersonID">The PersonID<see cref="int?"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static UserDTO GetUserByUserID(int UserID)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                {

                    using (SqlCommand cmd = new SqlCommand("Users_GetUserByUserID", connection))

                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        cmd.Parameters.AddWithValue("@UserID", UserID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                return _MapReaderToUserDTO(reader);
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

            return null;
        }

        /// <summary>
        /// The AddNewUserAsync
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="UserName">The UserName<see cref="string"/></param>
        /// <param name="Email">The Email<see cref="string"/></param>
        /// <param name="PasswordHash">The PasswordHash<see cref="string"/></param>
        /// <param name="ProfileImagePath">The ProfileImagePath<see cref="string"/></param>
        /// <param name="SpecialtyID">The SpecialtyID<see cref="int?"/></param>
        /// <param name="Points">The Points<see cref="int?"/></param>
        /// <param name="CreatedAt">The CreatedAt<see cref="DateTime?"/></param>
        /// <param name="UpdatedAt">The UpdatedAt<see cref="DateTime?"/></param>
        /// <param name="PersonID">The PersonID<see cref="int?"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public static async Task<int> AddNewUserAsync(UserDTO userDTO)
        {

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                {

                    using (SqlCommand cmd = new SqlCommand("Users_InsertNewUser", connection))

                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@UserName", userDTO.UserName);

                        cmd.Parameters.AddWithValue("@Email", userDTO.Email);

                        cmd.Parameters.AddWithValue("@PasswordHash", userDTO.PasswordHash);

                        cmd.Parameters.AddWithValue("@ProfileImagePath", (object)userDTO.ProfileImagePath ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@SpecialtyID", (object)userDTO.SpecialtyID ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@Points", (object)userDTO.Points ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@CreatedAt", (object)userDTO.CreatedAt ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@UpdatedAt", (object)userDTO.UpdatedAt ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@PersonID", (object)userDTO.PersonID ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        int newUserID = (int)cmd.Parameters["@NewUserID"].Value;
                        return newUserID;

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
        /// The UpdateUserAsync
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <param name="UserName">The UserName<see cref="string"/></param>
        /// <param name="Email">The Email<see cref="string"/></param>
        /// <param name="PasswordHash">The PasswordHash<see cref="string"/></param>
        /// <param name="ProfileImagePath">The ProfileImagePath<see cref="string"/></param>
        /// <param name="SpecialtyID">The SpecialtyID<see cref="int?"/></param>
        /// <param name="Points">The Points<see cref="int?"/></param>
        /// <param name="CreatedAt">The CreatedAt<see cref="DateTime?"/></param>
        /// <param name="UpdatedAt">The UpdatedAt<see cref="DateTime?"/></param>
        /// <param name="PersonID">The PersonID<see cref="int?"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> UpdateUserAsync(UserDTO userDTO)
        {
            bool IsRowsAffected = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                {

                    using (SqlCommand cmd = new SqlCommand("Users_UpdateUser", connection))

                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@UserID", userDTO.UserID);

                        cmd.Parameters.AddWithValue("@UserName", userDTO.UserName);

                        cmd.Parameters.AddWithValue("@Email", userDTO.Email);

                        cmd.Parameters.AddWithValue("@PasswordHash", userDTO.PasswordHash);

                        cmd.Parameters.AddWithValue("@ProfileImagePath", (object)userDTO.ProfileImagePath ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@SpecialtyID", (object)userDTO.SpecialtyID ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@Points", (object)userDTO.Points ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@CreatedAt", (object)userDTO.CreatedAt ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@UpdatedAt", (object)userDTO.UpdatedAt ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@PersonID", (object)userDTO.PersonID ?? DBNull.Value);
                        
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
        /// The DeleteUserAsync
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteUserAsync(int UserID)
        {
            bool IsRowsAffected = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                {

                    using (SqlCommand cmd = new SqlCommand("Users_DeleteUser", connection))

                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@UserID", UserID);
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
        /// The IsUserExistsAsync
        /// </summary>
        /// <param name="UserID">The UserID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsUserExistsAsync(int UserID)
        {

            bool IsFound = false;

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                {

                    using (SqlCommand cmd = new SqlCommand("Users_CheckUserExists", connection))

                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@UserID", UserID);

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
        /// The GetAllUsersAsync
        /// </summary>
        /// <returns>The <see cref="Task{DataTable}"/></returns>
        public static async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var userList = new List<UserDTO>();

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))

                {

                    using (SqlCommand cmd = new SqlCommand("Users_GetAllUsers", connection))

                    {

                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())

                        {

                            while (await reader.ReadAsync())
                                userList.Add(_MapReaderToUserDTO(reader));

                            
                        }

                    }

                }

            }

            catch (SqlException ex)
            {

                clsErrorEventLog.LogError(ex.Message);
                userList = null;
            }
            return userList;
        }

        public static LoginUserResponseDTO GetUserByEmailAndPassword(LoginRequestDTO requestDTO) 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    using (SqlCommand cmd = new SqlCommand("SP_GetUserByEmailOrUserAndPassword", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        cmd.Parameters.AddWithValue("@Email", requestDTO.Email);
                        cmd.Parameters.AddWithValue("@PasswordHash", requestDTO.PasswordHash);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                return new LoginUserResponseDTO(
                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Email")),
                                reader.IsDBNull(reader.GetOrdinal("ProfileImagePath")) ? null : reader.GetString(reader.GetOrdinal("ProfileImagePath")),
                                reader.IsDBNull(reader.GetOrdinal("SpecialtyID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SpecialtyID")),
                                reader.GetInt32(reader.GetOrdinal("Points")),
                                reader.GetInt32(reader.GetOrdinal("PersonID"))
                                );
                            }
                            return null;
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                clsErrorEventLog.LogError(ex.Message);
            }
            return null;
        }

    }
}
