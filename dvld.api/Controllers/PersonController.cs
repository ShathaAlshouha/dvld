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
            if (person == null)
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
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> AddPerson(clsPerson newPerson)
        {
            if (newPerson == null)
            {
                return BadRequest("Invalid person data.");
            }

            newPerson.Save();

            PersonDTO DTO = new PersonDTO
            {
                FirstName = newPerson.FirstName,
                SecondName = newPerson.SecondName,
                ThirdName = newPerson.ThirdName,
                LastName = newPerson.LastName,
                NationalNo = newPerson.NationalNo,
                DateOfBirth = newPerson.DateOfBirth,
                Gendor = newPerson.Gendor,
                Address = newPerson.Address,
                Phone = newPerson.Phone,
                Email = newPerson.Email,
                NationalityCountryID = newPerson.NationalityCountryID,
                ImagePath = newPerson.ImagePath
            };
            return Ok(DTO);
        }


        [HttpPut("{id}", Name = "Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> Update(int id, [FromBody] PersonDTO updatePerson)
        {
            if (id < 0)
            {
                return BadRequest("Invalid person data or ID.");
            }
            clsPerson person = clsPerson.Find(id);
            if (person == null)
            {
                return NotFound($"Person with ID {id} not found.");
            }
            person.FirstName = updatePerson.FirstName;
            person.SecondName = updatePerson.SecondName;
            person.ThirdName = updatePerson.ThirdName;
            person.LastName = updatePerson.LastName;
            person.NationalNo = updatePerson.NationalNo;
            person.DateOfBirth = updatePerson.DateOfBirth;
            person.Gendor = updatePerson.Gendor;
            person.Address = updatePerson.Address;
            person.Phone = updatePerson.Phone;
            person.Email = updatePerson.Email;
            person.NationalityCountryID = updatePerson.NationalityCountryID;
            person.ImagePath = updatePerson.ImagePath;

            person.Save();
            PersonDTO updatedDTO = new PersonDTO
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
            return Ok(updatedDTO);

        }

        [HttpGet("All")]
        public ActionResult<IEnumerable<PersonDTO>> GetAllPeople()
        {
            var people = clsPerson.GetAllPeople();

            if (people == null || people.Count == 0)
            {
                return NotFound("No people found.");
            }

            return Ok(people);
        }

        [HttpGet("{id}", Name = "IsExist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<bool> IsExist(int id)
        {
            if(id<0)
            {
                return BadRequest("Invalid person ID.");
            }
            clsPerson person = clsPerson.Find(id);
            if (person == null)
            {
                return NotFound($"Person with ID {id} not found.");
            }
            return Ok(true);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid person ID.");
            }

            clsPerson person = clsPerson.Find(id);
            if (person == null)
            {
                return NotFound($"Person with ID {id} not found.");
            }

            if (clsPerson.DeletePerson(id))
            {
                return Ok($"Person with ID {id} deleted successfully.");
            }

     
            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete the person.");
        }



        [HttpGet("{Nationalno}", Name = "IsExist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> IsExist(string Nationalno)
        {
            if (string.IsNullOrEmpty(Nationalno))
            {
                return BadRequest("Invalid National No.");
            }
            clsPerson person = clsPerson.Find(Nationalno);
            if (person == null)
            {
                return NotFound($"Person with National No {Nationalno} not found.");
            }
            return Ok(true);
        }

    }
}

