namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsCommentData" />
    /// </summary>
    public class clsCommentData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsCommentData"/> class.
        /// </summary>
        static clsCommentData()
        {
        }

        private static CommentDTO _MapReaderToCommentDTO(SqlDataReader reader)
        {
            try
            {
                return new CommentDTO(
                    reader.GetInt32(reader.GetOrdinal("CommentID")),
                    reader.GetInt32(reader.GetOrdinal("ProjectPostID")),
                    reader.GetInt32(reader.GetOrdinal("UserID")),
                    reader.IsDBNull(reader.GetOrdinal("Date")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("Date")),
                    reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? null : reader.GetString(reader.GetOrdinal("ImagePath"))
                );
            }
            catch (IndexOutOfRangeException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The GetCommentByCommentID
        /// </summary>
        /// <param name="CommentID">The CommentID<see cref="int"/></param>
        /// <returns>The <see cref="CommentDTO"/></returns>
        public static CommentDTO GetCommentByCommentID(int CommentID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Comments_GetCommentByCommentID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        cmd.Parameters.AddWithValue("@CommentID", CommentID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToCommentDTO(reader);
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
        /// The AddNewCommentAsync
        /// </summary>
        /// <param name="commentDTO">The commentDTO<see cref="CommentDTO"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public static async Task<int> AddNewCommentAsync(CommentDTO commentDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Comments_InsertNewComment", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@CommentID", commentDTO.CommentID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", commentDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@UserID", commentDTO.UserID);
                        cmd.Parameters.AddWithValue("@Date", (object)commentDTO.Date ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ImagePath", (object)commentDTO.ImagePath ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewCommentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        int newCommentID = (int)cmd.Parameters["@NewCommentID"].Value;
                        return newCommentID;
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
        /// The UpdateCommentAsync
        /// </summary>
        /// <param name="commentDTO">The commentDTO<see cref="CommentDTO"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> UpdateCommentAsync(CommentDTO commentDTO)
        {
            bool IsRowsAffected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Comments_UpdateComment", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@CommentID", commentDTO.CommentID);
                        cmd.Parameters.AddWithValue("@ProjectPostID", commentDTO.ProjectPostID);
                        cmd.Parameters.AddWithValue("@UserID", commentDTO.UserID);
                        cmd.Parameters.AddWithValue("@Date", (object)commentDTO.Date ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ImagePath", (object)commentDTO.ImagePath ?? DBNull.Value);

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
        /// The DeleteCommentAsync
        /// </summary>
        /// <param name="CommentID">The CommentID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeleteCommentAsync(int CommentID)
        {
            bool IsRowsAffected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Comments_DeleteComment", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@CommentID", CommentID);
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
        /// The IsCommentExistsAsync
        /// </summary>
        /// <param name="CommentID">The CommentID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsCommentExistsAsync(int CommentID)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Comments_CheckCommentExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@CommentID", CommentID);

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
        /// The GetAllCommentsAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{CommentDTO}}"/></returns>
        public static async Task<List<CommentDTO>> GetAllCommentsAsync()
        {
            var commentList = new List<CommentDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Comments_GetAllComments", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var comment = _MapReaderToCommentDTO(reader);
                                if (comment != null)
                                {
                                    commentList.Add(comment);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                commentList = null;
            }
            return commentList;
        }
    }
}