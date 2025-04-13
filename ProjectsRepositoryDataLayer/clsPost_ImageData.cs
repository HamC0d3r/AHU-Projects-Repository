namespace ProjectsRepositoryDB_DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="clsPost_ImageData" />
    /// </summary>
    public class clsPost_ImageData
    {
        /// <summary>
        /// Initializes static members of the <see cref="clsPost_ImageData"/> class.
        /// </summary>
        static clsPost_ImageData()
        {
        }

        private static PostImageDTO _MapReaderToPostImageDTO(SqlDataReader reader)
        {
            try
            {
                return new PostImageDTO(
                    reader.GetInt32(reader.GetOrdinal("ImagePostID")),
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
        /// The GetPost_ImageByImagePostID
        /// </summary>
        /// <param name="ImagePostID">The ImagePostID<see cref="int"/></param>
        /// <returns>The <see cref="PostImageDTO"/></returns>
        public static PostImageDTO GetPost_ImageByImagePostID(int ImagePostID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Image_GetPost_ImageByImagePostID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        cmd.Parameters.AddWithValue("@ImagePostID", ImagePostID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapReaderToPostImageDTO(reader);
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
        /// The AddNewPost_ImageAsync
        /// </summary>
        /// <param name="postImageDTO">The postImageDTO<see cref="PostImageDTO"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public static async Task<int> AddNewPost_ImageAsync(PostImageDTO postImageDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Image_InsertNewPost_Image", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@ImagePath", (object)postImageDTO.ImagePath ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewImagePostID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        cmd.Parameters.Add(outputIdParam);
                        await cmd.ExecuteNonQueryAsync();
                        int newImagePostID = (int)cmd.Parameters["@NewImagePostID"].Value;
                        return newImagePostID;
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
        /// The UpdatePost_ImageAsync
        /// </summary>
        /// <param name="postImageDTO">The postImageDTO<see cref="PostImageDTO"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> UpdatePost_ImageAsync(PostImageDTO postImageDTO)
        {
            bool IsRowsAffected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Image_UpdatePost_Image", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@ImagePostID", postImageDTO.ImagePostID);
                        cmd.Parameters.AddWithValue("@ImagePath", (object)postImageDTO.ImagePath ?? DBNull.Value);

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
        /// The DeletePost_ImageAsync
        /// </summary>
        /// <param name="ImagePostID">The ImagePostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> DeletePost_ImageAsync(int ImagePostID)
        {
            bool IsRowsAffected = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Image_DeletePost_Image", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@ImagePostID", ImagePostID);
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
        /// The IsPost_ImageExistsAsync
        /// </summary>
        /// <param name="ImagePostID">The ImagePostID<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public static async Task<bool> IsPost_ImageExistsAsync(int ImagePostID)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Image_CheckPost_ImageExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        cmd.Parameters.AddWithValue("@ImagePostID", ImagePostID);

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
        /// The GetAllPost_ImageAsync
        /// </summary>
        /// <returns>The <see cref="Task{List{PostImageDTO}}"/></returns>
        public static async Task<List<PostImageDTO>> GetAllPost_ImageAsync()
        {
            var postImageList = new List<PostImageDTO>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Post_Image_GetAllPost_Image", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var postImage = _MapReaderToPostImageDTO(reader);
                                if (postImage != null)
                                {
                                    postImageList.Add(postImage);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsErrorEventLog.LogError(ex.Message);
                postImageList = null;
            }
            return postImageList;
        }
    }
}