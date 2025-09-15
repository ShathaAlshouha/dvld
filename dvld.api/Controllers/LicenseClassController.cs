using DTOs;
using dvld.business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public class LicenseClassController : ControllerBase
    {
        [HttpGet("All")]
        public ActionResult<IEnumerable<LicenseClassDTO>> Get()
        {
            var licenseClassDTO = clsLicenseClass.GetAllLicenseClasses();
            if (licenseClassDTO == null)
            {
                return NotFound("Not found");
            }
            return Ok(licenseClassDTO);
        }

    }
}
