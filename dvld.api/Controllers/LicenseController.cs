using DTOs;
using dvld.business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class LicenseController : ControllerBase
    {

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<LicenseDTO>> GetAllLicense()
        {
            var Licenses = clsLisence.GetAllLicenses();
            if (Licenses == null || Licenses.Count == 0)
                return NotFound("No Licenses found.");
            return Ok(Licenses);
        }
        [HttpGet("GetByID/{id}")]
        public ActionResult<LicenseDTO> GetLicenseByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID supplied.");
            }
            var license = clsLisence.GetLicenseByID(id);
            if (license == null)
                return NotFound($"License with ID {id} not found.");
            return Ok(license);
        }

        [HttpGet("{LicenseClassID}GetActiveLisence/{personID}")]
        public ActionResult<int> GetActiveLisence(int personID, int LicenseClassID)
        {
            if (personID <= 0)
            {
                return BadRequest("Invalid ID supplied.");
            }
            var license = clsLisence.GetActiveLicenseIDByPersonID(personID, LicenseClassID);
            return license;
        }

    }

}
