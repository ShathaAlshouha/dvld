using DTOs;
using dvld.business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetaindLicenseController : ControllerBase
    {
        [HttpGet("All")]
        public ActionResult<IEnumerable<DetainedLicenseDTO>> GetAllDetainedLicenses()
        {
            var detainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();
            if (detainedLicenses == null || detainedLicenses.Count == 0)
                return NotFound("No detained licenses found.");
            return Ok(detainedLicenses);
        }


        [HttpGet("GetByLicenseID/{id}")]
        public ActionResult<DetainedLicenseDTO> GetDetainedLicenseByLicenseID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID supplied.");
            }
            var detainedLicense = clsDetainedLicense.FindByLicenseID(id);
            if (detainedLicense == null)
                return NotFound($"Detained license with ID {id} not found.");
            return Ok(detainedLicense);
        }


        [HttpGet("GetByID/{id}")]
        public ActionResult<DetainedLicenseDTO> GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID supplied.");
            }
            var detainedLicense = clsDetainedLicense.Find(id);
            if (detainedLicense == null)
                return NotFound($"Detained license with ID {id} not found.");
            return Ok(detainedLicense);
        }
    
    }
}
