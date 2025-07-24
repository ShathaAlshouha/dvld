using DTOs;
using dvld.business;
using dvld.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet("ByID/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> GetPersonByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Person ID.");
            }
            clsPerson person = clsPerson.Find(id);
            if (person == null)
            {
                return NotFound($"Person with ID {id} not found.");
            }

            var personDTO = new PersonDTO
            {
                PersonID = person.PersonID,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                ThirdName = person.ThirdName,
                LastName = person.LastName,
                NationalNo = person.NationalNo,
                DateOfBirth = person.DateOfBirth,
                Gendor = person.Gendor,
                Address = person.Address,
                Phone = person.Phone,
                Email = person.Email,
                NationalityCountryID = person.NationalityCountryID,
                ImagePath = person.ImagePath
            };

            return Ok(personDTO);
        }

        [HttpGet("ByNationalNo/{nationalNo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
 
        public ActionResult<PersonDTO> GetPersonByNationalNo(string nationalNo)
        {
            clsPerson person = clsPerson.Find(nationalNo); 
            if(person == null)
            {
                return NotFound($"person with{nationalNo} not found"); 
            }
            var personDTO = new PersonDTO
            {
                PersonID = person.PersonID,
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                ThirdName = person.ThirdName,
                LastName = person.LastName,
                NationalNo = person.NationalNo,
                DateOfBirth = person.DateOfBirth,
                Gendor = person.Gendor,
                Address = person.Address,
                Phone = person.Phone,
                Email = person.Email,
                NationalityCountryID = person.NationalityCountryID,
                ImagePath = person.ImagePath
            };
            return Ok(personDTO); 

        }
    }
}

