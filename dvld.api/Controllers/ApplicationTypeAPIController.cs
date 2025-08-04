using DTOs;
using dvld.business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationTypeAPIController : ControllerBase
    {
        [HttpGet("ApplicationTypeByID/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public ActionResult<ApplicationTypeDTO> GetApplicationTypeByID(int id)
        {

            if (id < 0)
            {
                return BadRequest("Invalid ID provided.");
            }
            clsApplicationType ApplicationType = clsApplicationType.Find(id);
            if (ApplicationType == null)
            {
                return NotFound($"Application Type with ID {id} not found.");
            }
            ApplicationTypeDTO DTO = new ApplicationTypeDTO
            {
                ID = ApplicationType.ID,
                Title = ApplicationType.Title,
                Fees = ApplicationType.Fees
            };
            return Ok(DTO);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <IEnumerable<ApplicationTypeDTO>> GetAll()
        {
            List<ApplicationTypeDTO> ApplicationTyps = clsApplicationType.GetAllApplicationTypes();
            if (ApplicationTyps == null )
            {
                return NotFound("No application types found.");
            }
            return Ok(ApplicationTyps);
        }
    }
}
