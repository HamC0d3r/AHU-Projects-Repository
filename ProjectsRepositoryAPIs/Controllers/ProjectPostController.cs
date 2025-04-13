using Microsoft.AspNetCore.Mvc;
using ProjectsRepositoryDB_Business;
using ProjectsRepositoryDB_DataAccess;
using ProjectsRepositoryDB_DataAccess.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Linq;

namespace ProjectsRepositoryAPIs.Controllers
{
    [Route("api/ProjectPosts")]
    [ApiController]
    public class ProjectPostsController : ControllerBase
    {
        // Existing endpoints remain the same...

        [HttpGet("All", Name = "GetAllProjectPosts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProjectPostDTO>>> GetAllProjectPosts()
        {
            List<ProjectPostDTO> allProjectPosts = await clsProjectPost.GetAllProjectPostAsync();

            if (allProjectPosts == null || allProjectPosts.Count == 0)
            {
                return NotFound("No project posts found.");
            }

            return Ok(allProjectPosts);
        }

        [HttpGet("{id}", Name = "GetProjectPostByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProjectPostDTO> GetProjectPostByID(int id)
        {
            if (id < 0)
            {
                return BadRequest($"Invalid ID: {id}. ID must be a positive integer.");
            }

            clsProjectPost projectPost = clsProjectPost.Find(id);

            if (projectPost == null)
            {
                return NotFound($"Project post with ID {id} not found.");
            }

            return Ok(projectPost.projectPostDTO);
        }

        [HttpPost("Add", Name = "AddProjectPost")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProjectPostDTO>> AddProjectPost([FromBody] ProjectPostDTO projectPostDTO)
        {
            if (projectPostDTO == null || string.IsNullOrWhiteSpace(projectPostDTO.Title) || string.IsNullOrWhiteSpace(projectPostDTO.Body))
            {
                return BadRequest("Project post data is invalid. Title and Body are required.");
            }

            if (projectPostDTO.UserID <= 0)
            {
                return BadRequest("UserID must be a positive integer.");
            }

            clsProjectPost projectPost = new clsProjectPost
            {
                Title = projectPostDTO.Title,
                Body = projectPostDTO.Body,
                ImagePostID = projectPostDTO.ImagePostID,
                UserID = projectPostDTO.UserID,
                LinkID = projectPostDTO.LinkID,
                TypePostID = projectPostDTO.TypePostID,
                CommentsNum = projectPostDTO.CommentsNum,
                LikesNum = projectPostDTO.LikesNum,
                ContributorsNum = projectPostDTO.ContributorsNum,
                CreatedAt = projectPostDTO.CreatedAt ?? DateTime.Now,
                UpdatedAt = projectPostDTO.UpdatedAt
            };

            if (await projectPost.Save())
            {
                projectPostDTO.ProjectPostID = projectPost.ProjectPostID;
                return CreatedAtRoute("GetProjectPostByID", new { id = projectPost.ProjectPostID }, projectPostDTO);
            }

            return BadRequest("Failed to add project post.");
        }

        [HttpPut("Update/{id}", Name = "UpdateProjectPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProjectPostDTO>> UpdateProjectPost(int id, ProjectPostDTO projectPostDTO)
        {
            if (id < 0 || projectPostDTO == null)
            {
                return BadRequest($"Invalid request. ID {id} must match the ProjectPostID in the DTO and must be a positive integer.");
            }

            clsProjectPost projectPost = clsProjectPost.Find(id);
            if (projectPost == null)
            {
                return NotFound($"Project post with ID {id} not found.");
            }

            if (string.IsNullOrWhiteSpace(projectPostDTO.Title) || string.IsNullOrWhiteSpace(projectPostDTO.Body))
            {
                return BadRequest("Title and Body are required for updating.");
            }

            projectPost.Title = projectPostDTO.Title;
            projectPost.Body = projectPostDTO.Body;
            projectPost.ImagePostID = projectPostDTO.ImagePostID;
            projectPost.UserID = projectPostDTO.UserID;
            projectPost.LinkID = projectPostDTO.LinkID;
            projectPost.TypePostID = projectPostDTO.TypePostID;
            projectPost.CommentsNum = projectPostDTO.CommentsNum;
            projectPost.LikesNum = projectPostDTO.LikesNum;
            projectPost.ContributorsNum = projectPostDTO.ContributorsNum;
            projectPost.CreatedAt = projectPostDTO.CreatedAt;
            projectPost.UpdatedAt = projectPostDTO.UpdatedAt ?? DateTime.Now;

            if (await projectPost.Save())
            {
                return Ok(projectPost.projectPostDTO);
            }

            return BadRequest("Failed to update project post.");
        }

        [HttpDelete("Delete/{id}", Name = "DeleteProjectPost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProjectPost(int id)
        {
            if (id < 0)
            {
                return BadRequest($"Invalid ID: {id}. ID must be a positive integer.");
            }

            if (!await clsProjectPost.IsProjectPostExistsAsync(id))
            {
                return NotFound($"Project post with ID {id} not found.");
            }

            if (await clsProjectPost.DeleteProjectPostAsync(id))
            {
                return NoContent();
            }

            return BadRequest($"Failed to delete project post with ID {id}.");
        }

        // New Endpoint: Upload Image for a Project Post
        [HttpPost("UploadImage/", Name = "UploadProjectPostImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadProjectPostImage(IFormFile imageFile)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Invalid file type. Only .jpg, .jpeg, .png files are allowed.");
            }

            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file upload");

            const long maxFileSize = 5 * 1024 * 1024; // 5MB in bytes
            if (imageFile.Length > maxFileSize)
            {
                return BadRequest($"File size must not exceed {maxFileSize / 1024 / 1024}MB.");
            }

            var uploadDirectory = @"C:\MyUploads";

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadDirectory, fileName);

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            clsPost_Image Post_Image = new clsPost_Image { ImagePath = filePath };
            int imagePostId = -1;
            if (await Post_Image.Save())
            {
                imagePostId = Post_Image.ImagePostID;
            }
            return Ok( new { imageID = imagePostId, FilePath = filePath } );
            return BadRequest("Invaled");
        }

         
    }
}