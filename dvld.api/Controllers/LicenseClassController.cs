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
        public ActionResult<LicenseClassDTO> GetByID(int id)
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

        [HttpGet("{classname}/")]
        public ActionResult<LicenseClassDTO> GetByClassName(int classname)
        {
            if (classname <= 0)
            {
                return BadRequest("");
            }
            var licensClss = clsLicenseClass.Find(classname);
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

        [HttpPost("Create")]
        public ActionResult<LicenseClassDTO> Create([FromBody] LicenseClassDTO licenseClassDTO)
        {

            if (licenseClassDTO == null)
            {
                return BadRequest("");
            }

            clsLicenseClass licenseClass = new clsLicenseClass
            {
                ClassDescription = licenseClassDTO.ClassDescription,
                ClassFees = licenseClassDTO.ClassFees,
                ClassName = licenseClassDTO.ClassName,
                DefaultValidityLength = licenseClassDTO.DefaultValidityLength,
                MinimumAllowedAge = licenseClassDTO.MinimumAllowedAge

            };
            if (!licenseClass.Save())
            {

                return BadRequest("Could not create License Class");
            }
            LicenseClassDTO dto = new LicenseClassDTO
            {
                LicenseClassID = licenseClass.LicenseClassID,
                ClassDescription = licenseClass.ClassDescription,
                ClassFees = licenseClass.ClassFees,
                ClassName = licenseClass.ClassName,
                DefaultValidityLength = licenseClass.DefaultValidityLength,
                MinimumAllowedAge = licenseClass.MinimumAllowedAge
            };
            return Ok(dto);


        }

        [HttpPut("Update{id}")]
        public ActionResult<LicenseClassDTO> Update(int id, [FromBody] LicenseClassDTO licenseClassDTO)
        {

            if (licenseClassDTO == null || id <= 0)
            {
                return BadRequest("");
            }

            clsLicenseClass licenseClass = clsLicenseClass.Find(licenseClassDTO.LicenseClassID);
            if (licenseClass == null)
            {
                return NotFound("License Class Not Found");
            }
            licenseClass.ClassDescription = licenseClassDTO.ClassDescription;
            licenseClass.ClassFees = licenseClassDTO.ClassFees;
            licenseClass.ClassName = licenseClassDTO.ClassName;
            licenseClass.DefaultValidityLength = licenseClassDTO.DefaultValidityLength;
            licenseClass.MinimumAllowedAge = licenseClassDTO.MinimumAllowedAge;

            if (!licenseClass.Save())
            {

                return BadRequest("Could not update License Class");
            }
            LicenseClassDTO dto = new LicenseClassDTO
            {
                LicenseClassID = licenseClass.LicenseClassID,
                ClassDescription = licenseClass.ClassDescription,
                ClassFees = licenseClass.ClassFees,
                ClassName = licenseClass.ClassName,
                DefaultValidityLength = licenseClass.DefaultValidityLength,
                MinimumAllowedAge = licenseClass.MinimumAllowedAge
            };
            return Ok(dto);
        }
    }
}
