namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsLikeData" />
    /// </summary>
    public class clsLikeData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsLikeData"/> class.
        /// </summary>
        static clsLikeData()
        {
        }

        private static LikeDTO _MapReaderToLikeDTO(SqlDataReader reader)
        {
            try
            {
                return new LikeDTO(
                    reader.GetInt32(reader.GetOrdinal("LikeID")),
                    reader.GetInt32(reader.GetOrdinal("ProjectPostID")),
                    reader.GetInt32(reader.GetOrdinal("UserID")),
                    reader.IsDBNull(reader.GetOrdinal("Date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Date")),
                    reader.IsDBNull(reader.GetOrdinal("TypeOfLike")) ? null : reader.GetString(reader.GetOrdinal("TypeOfLike"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The GetLikeByLikeID
        /// </summary>
        /// <param name="LikeID">The LikeID<see cref="int"/></param>
        /// <returns>The <see cref="LikeDTO"/></returns>
        public static LikeDTO GetLikeByLikeID(int LikeID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Likes_GetLikeByLikeID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        cmd.Parameters.AddWithValue("@LikeID", LikeID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToLikeDTO(reader);
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
        /// The AddNewLikeAsync
        /// </summary>
        /// <param name="likeDTO">The likeDTO<see cref="LikeDTO"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public static async Task<int> AddNewLikeAsync(LikeDTO likeDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Likes_InsertNewLike", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@LikeID", likeDTO.LikeID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", likeDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@UserID", likeDTO.UserID);
                        cmd.Parameters.AddWithValue("@Date", (object)likeDTO.Date ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TypeOfLike", (object)likeDTO.TypeOfLike ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewLikeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        int newLikeID = (int)cmd.Parameters["@NewLikeID"].Value;
                        return newLikeID;
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
        /// The UpdateLikeAsync
        /// </summary>
        /// <param name="likeDTO">The likeDTO<see cref="LikeDTO"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> UpdateLikeAsync(LikeDTO likeDTO)
        {
            bool IsRowsAffected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Likes_UpdateLike", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@LikeID", likeDTO.LikeID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", likeDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@UserID", likeDTO.UserID);
                        cmd.Parameters.AddWithValue("@Date", (object)likeDTO.Date ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TypeOfLike", (object)likeDTO.TypeOfLike ?? DBNull.Value);

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
        /// The DeleteLikeAsync
        /// </summary>
        /// <param name="LikeID">The LikeID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteLikeAsync(int LikeID)
        {
            bool IsRowsAffected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Likes_DeleteLike", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@LikeID", LikeID);
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
        /// The IsLikeExistsAsync
        /// </summary>
        /// <param name="LikeID">The LikeID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsLikeExistsAsync(int LikeID)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Likes_CheckLikeExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@LikeID", LikeID);

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
        /// The GetAllLikesAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{LikeDTO}}"/></returns>
        public static async Task<List<LikeDTO>> GetAllLikesAsync()
        {
            var likeList = new List<LikeDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Likes_GetAllLikes", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var like = _MapReaderToLikeDTO(reader);
                                if (like != null)
                                {
                                    likeList.Add(like);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                likeList = null;
            }
            return likeList;
        }
    }
}