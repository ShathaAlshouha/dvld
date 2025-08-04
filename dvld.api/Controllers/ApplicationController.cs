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
    }
}
