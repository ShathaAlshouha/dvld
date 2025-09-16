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
    }

}
