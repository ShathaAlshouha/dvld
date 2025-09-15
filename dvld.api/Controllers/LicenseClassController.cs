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
        [HttpGet("{id}")]
        public ActionResult<LicenseClassDTO> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("");
            }
            var licensClss = clsLicenseClass.Find(id);
            if (licensClss == null)
            {
                return NotFound("LicensClss NotFound");
            }
            var LisencClassDTO = new LicenseClassDTO
            {
                LicenseClassID = licensClss.LicenseClassID,
                DefaultValidityLength = licensClss.DefaultValidityLength,
                ClassDescription = licensClss.ClassDescription,
                ClassFees = licensClss.ClassFees,
                MinimumAllowedAge = licensClss.MinimumAllowedAge,
                ClassName = licensClss.ClassName

            };
            return Ok(LisencClassDTO);
        }
    }
}
