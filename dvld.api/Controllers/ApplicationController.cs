using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using dvld.business;
namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        [HttpGet("GetApplication/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOs.ApplicationDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ApplicationDTO> GetByID(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Application ID");
            }
            clsApplication application = clsApplication.FindBaseApplication(id);
            if (application == null)
            {
                return NotFound($"Application with ID {id} not found.");
            }
            ApplicationDTO DTO = new ApplicationDTO
            {
                ApplicationID = application.ApplicationID,
                ApplicantPersonID = application.ApplicantPersonID,
                ApplicationTypeID = application.ApplicationTypeID,
                ApplicationStatus = (byte)application.ApplicationStatus,
                LastStatusDate = application.LastStatusDate,
                PaidFees = application.PaidFees,
                CreatedByUserID = application.CreatedByUserID,

            };

            return Ok(DTO);
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ApplicationDTO>> GetAllApplications()
        {
            List<ApplicationDTO> applicationDTOs = clsApplication.GetAllApplications();
            if (applicationDTOs == null)
            {
                return NotFound("No applications found.");
            }
            return Ok(applicationDTOs);
        }


        [HttpGet("IsExist/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOs.ApplicationDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> IsExist(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Application ID");
            }
            bool exists = clsApplication.IsApplicationExist(id);
            if (!exists)
            {
                return NotFound($"Application with ID {id} does not exist.");
            }
            return Ok(exists);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DTOs.ApplicationDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ApplicationDTO> AddApplication([FromBody] ApplicationDTO applicationDTO)
        {
           if(applicationDTO==null)
            {
                return BadRequest("Application data is required.");
            }
            clsApplication application = new clsApplication
            {
                ApplicantPersonID = applicationDTO.ApplicantPersonID,
                ApplicationDate = applicationDTO.ApplicationDate,
                ApplicationTypeID = applicationDTO.ApplicationTypeID,
                ApplicationStatus = (clsApplication.enApplicationStatus)applicationDTO.ApplicationStatus,
                LastStatusDate = applicationDTO.LastStatusDate,
                PaidFees = applicationDTO.PaidFees,
                CreatedByUserID = applicationDTO.CreatedByUserID
            };

            bool saved = application.Save();
            if (!saved)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to save application.");
            }

            var resultDTO = new ApplicationDTO
            {
                ApplicationID = application.ApplicationID,
                ApplicantPersonID = application.ApplicantPersonID,
                ApplicationDate = application.ApplicationDate,
                ApplicationTypeID = application.ApplicationTypeID,
                ApplicationStatus = (byte)application.ApplicationStatus,
                LastStatusDate = application.LastStatusDate,
                PaidFees = application.PaidFees,
                CreatedByUserID = application.CreatedByUserID
            };

            return Ok(resultDTO);
        }

        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DTOs.ApplicationDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> UpdateApplication(int id, [FromBody] ApplicationDTO applicationDTO)
        {
            if (id < 1)
            {
                return BadRequest("Invalid Application ID");
            }
            clsApplication application = clsApplication.FindBaseApplication(id);
            if (application == null)
            {
                return NotFound($"Application with ID {id} not found.");
            }
            application.ApplicationID = id;
            application.ApplicationTypeID = applicationDTO.ApplicationTypeID;
            application.ApplicantPersonID = applicationDTO.ApplicantPersonID;
            application.ApplicationDate = applicationDTO.ApplicationDate;
            application.ApplicationStatus = (clsApplication.enApplicationStatus)applicationDTO.ApplicationStatus;
            application.LastStatusDate = applicationDTO.LastStatusDate;
            application.PaidFees = applicationDTO.PaidFees;
            application.CreatedByUserID = applicationDTO.CreatedByUserID;
            bool updated = application.Save();
            if (!updated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update application.");
            }
            ApplicationDTO DTO = new ApplicationDTO
            {
                ApplicationTypeID = application.ApplicationTypeID,
                ApplicantPersonID = application.ApplicantPersonID,
                ApplicationDate = application.ApplicationDate,
                ApplicationStatus = (byte)application.ApplicationStatus,
                LastStatusDate = application.LastStatusDate,
                PaidFees = application.PaidFees,
                CreatedByUserID = application.CreatedByUserID,
                ApplicationID = application.ApplicationID

            };

            return Ok($"Application with ID {id} Updated successfully.");
        }
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> DeleteApplication(int id)
        {

            if (id < 1)
            {
                return BadRequest("Invalid Application ID");
            }

            clsApplication application = clsApplication.FindBaseApplication(id);
            if (application == null)
            {
                return NotFound($"Application with ID {id} not found.");
            }
            bool deleted = application.Delete();
            if (!deleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to delete application with ID {id}.");
            }

            return Ok($"Application with ID {id} deleted successfully.");
        }

    }
}
