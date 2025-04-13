using Microsoft.AspNetCore.Mvc;
using ProjectsRepositoryDB_Business;
using ProjectsRepositoryDB_DataAccess;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectsRepositoryDB_DataAccess.DTOs;
using ProjectsRepository_Business;

namespace ProjectsRepositoryAPIs.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            List<UserDTO> allUsers = await clsUser.GetAllUsersAsync();

            if (allUsers == null || allUsers.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(allUsers);
        }

        [HttpGet("{id}", Name = "GetUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByID(int id)
        {
            if (id < 0)
            {
                return BadRequest($"Invalid ID: {id}. ID must be a positive integer.");
            }

            clsUser user = clsUser.Find(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            UserDTO userDTO = user.userDTO;
            return Ok(userDTO);
        }

        [HttpPost("Add", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null || string.IsNullOrWhiteSpace(userDTO.UserName) ||
                string.IsNullOrWhiteSpace(userDTO.Email) || string.IsNullOrWhiteSpace(userDTO.PasswordHash))
            {
                return BadRequest("User data is invalid. UserName, Email, and PasswordHash are required.");
            }

            clsUser user = new clsUser
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                PasswordHash = userDTO.PasswordHash,
                ProfileImagePath = userDTO.ProfileImagePath,
                SpecialtyID = userDTO.SpecialtyID,
                Points = userDTO.Points,
                CreatedAt = userDTO.CreatedAt ?? DateTime.Now, // Default to now if not provided
                UpdatedAt = userDTO.UpdatedAt,
                PersonID = userDTO.PersonID
            };

            if (await user.Save())
            {
                userDTO.UserID = user.UserID; // Update the DTO with the new ID
                return CreatedAtRoute("GetUserByID", new { id = user.UserID }, userDTO);
            }

            return BadRequest("Failed to add user.");
        }

        [HttpPut("Update/{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, UserDTO userDTO)
        {
            if (id < 0 || userDTO == null )
            {
                return BadRequest($"Invalid request. ID {id} must match the UserID in the DTO and must be a positive integer.");
            }

            clsUser user = clsUser.Find(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            // Update the user object with DTO values
            user.UserName = userDTO.UserName;
            user.Email = userDTO.Email;
            user.PasswordHash = userDTO.PasswordHash;
            user.ProfileImagePath = userDTO.ProfileImagePath;
            user.SpecialtyID = userDTO.SpecialtyID;
            user.Points = userDTO.Points;
            user.CreatedAt = userDTO.CreatedAt ;
            user.UpdatedAt = userDTO.UpdatedAt ?? DateTime.Now; // Update timestamp
            user.PersonID = userDTO.PersonID;

            if (await user.Save())
            {
                return Ok(user.userDTO);
            }

            return BadRequest("Failed to update user.");
        }

        [HttpDelete("Delete/{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id < 0 )
            {
                return BadRequest($"Invalid ID: {id}. ID must be a positive integer.");
            }

            if (!await clsUser.IsUserExistsAsync(id))
            {
                return NotFound($"User with ID {id} not found.");
            }

            if (await clsUser.DeleteUserAsync(id))
            {
                return NoContent();
            }

            return BadRequest($"Failed to delete user with ID {id}.");
        }

        [HttpGet("Login")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> Login([FromQuery] LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data in Request is invalid");
            }


            try
            {
                Global.CurrentUser = clsUser.Login(request);

                if (Global.CurrentUser != null)
                {
                    return Ok(Global.CurrentUser);
                }

                return NotFound("User Not Found With Given credintials");
                
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "an unexpected error occurred");
            }
        }

        [HttpPost("Upload" , Name = "ProfileImageUpload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            // Validate file type (only images allowed)
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Invalid file type. Only .jpg, .jpeg, .png files are allowed.");
            }

            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("No file upload");

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
            return Ok(new { filePath });

        }
        [HttpGet("GetImage/{fileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetImage(string fileName)
        {
            var uploadDirectory = @"C:\MyUploads";
            var filePath = Path.Combine(uploadDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Image not found.");

            var image = System.IO.File.OpenRead(filePath);
            var mimeType = GetMimeType(filePath);

            return File(image, mimeType);
        }
        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",

            };
        }
    }
}