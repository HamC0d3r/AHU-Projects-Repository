using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectsRepositoryDB_Business;
using ProjectsRepositoryDB_DataAccess;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ProjectsRepository_APIs.Controllers
{
    [Route("api/Person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<PersonDTO>>> GetAllPerson()
        {
            List<PersonDTO> ListOfPeople = await clsPerson.GetAllPersonAsync();
            if (ListOfPeople != null)
            {
                return Ok(ListOfPeople);
            }

            return NotFound("No People founded.");

        }
        [HttpGet("{id}", Name = "GetPersonByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetPersonByID(int id)
        {
            if (id < 0)
            {
                return BadRequest($"Invalid ID: {id}. ID must be a positive integer.");
            }

            clsPerson person = clsPerson.Find(id);

            if (person == null)
            {
                return NotFound($"Person with ID {id} not found.");
            }

            PersonDTO personDTO = person.personDTO;
            return Ok(personDTO);
        }


        [HttpPost("Add", Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PersonDTO>> AddPerson(PersonDTO personDTO)
        {
            if (personDTO == null || personDTO.UniversityID == 0 || 
                string.IsNullOrEmpty(personDTO.FirstName) || string.IsNullOrEmpty(personDTO.LastName))
            {
                return BadRequest("Person data is invalid.");
            }

            clsPerson person = new clsPerson
            {
                    PersonID = personDTO.PersonID,
                    FirstName = personDTO.FirstName,
                    SecondName = personDTO.SecondName,
                    ThirdName = personDTO.ThirdName,
                    LastName = personDTO.LastName,
                    UniversityID = personDTO.UniversityID,
                    ContactEmail = personDTO.ContactEmail,
                    IsEmployee = personDTO.IsEmployee,
                    CreatedAt = personDTO.CreatedAt ?? DateTime.Now,
                    UpdatedAt = personDTO.UpdatedAt ,
                    Gendor = personDTO.Gendor 
            };

            if (await person.Save()) 
            {
                personDTO.PersonID = person.PersonID; // Update the DTO with the new ID
                return CreatedAtRoute("AddPerson", new { id = personDTO.PersonID }, personDTO);
            }

            return BadRequest("Failed to Person user.");
        }

        [HttpPut("Update/{id}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonDTO>> UpdatePerson(int id , PersonDTO personDTO)
        {
            if (personDTO == null || id < 0 || personDTO.UniversityID == 0 ||
                string.IsNullOrEmpty(personDTO.FirstName) || string.IsNullOrEmpty(personDTO.LastName))
            {
                return BadRequest("Person data is invalid.");
            }

            clsPerson person = clsPerson.Find(id);
            if (person == null) 
            {
                return NotFound($"Person with ID {id} not found.");
            }

            person.FirstName = personDTO.FirstName;
            person.SecondName = personDTO.SecondName;
            person.ThirdName = personDTO.ThirdName;
            person.LastName = personDTO.LastName;
            person.UniversityID = personDTO.UniversityID;
            person.ContactEmail = personDTO.ContactEmail;
            person.IsEmployee = personDTO.IsEmployee;
            person.CreatedAt = personDTO.CreatedAt;
            person.UpdatedAt = personDTO.UpdatedAt ?? DateTime.Now;
            person.Gendor = personDTO.Gendor;

            if (await person.Save())
                return Ok(person.personDTO);
            else
                return StatusCode(500, new { message = "Error Updating Student" });
        }

      
    }

}
