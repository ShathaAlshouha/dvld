using DTOs;
using dvld.business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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

        [HttpGet("GetDriverLicenses/{driverID}")]
        public ActionResult<IEnumerable<LicenseViewDTO>> GetDriverLicenses(int driverID)
        {
            if (driverID <= 0)
            {
                return BadRequest("Invalid ID supplied.");
            }
            var licenses = clsLisence.GetDriverLicenses(driverID);
            if (licenses == null || licenses.Count == 0)
                return NotFound("No Licenses found for the specified driver.");
            return Ok(licenses);
        }
        [HttpPost("Create")]
        public ActionResult CreateLicense([FromBody] LicenseDTO licenseDto)
        {
            if (licenseDto == null)
            {
                return BadRequest("License data is null.");
            }
            var license = new clsLisence
            {
                PaidFees = licenseDto.PaidFees,
                IsActive = licenseDto.IsActive,
                Notes = licenseDto.Notes,
                DriverID = licenseDto.DriverID,
                ApplicationID = licenseDto.ApplicationID,
                IssueDate = licenseDto.IssueDate,
                ExpirationDate = licenseDto.ExpirationDate,
                IssueReason = (clsLisence.enIssueReason)licenseDto.IssueReason,
                LicenseClass = licenseDto.LicenseClass,
                CreatedByUserID = licenseDto.CreatedByUserID
            };

            bool isCreated = license.Save();
            if (isCreated)
            {
                var dto = new LicenseDTO
                {
                    LicenseID = license.LicenseID,
                    LicenseClass = license.LicenseClass,
                    DriverID = license.DriverID,
                    ApplicationID = license.ApplicationID,
                    IssueDate = license.IssueDate,
                    ExpirationDate = license.ExpirationDate,
                    Notes = license.Notes,
                    PaidFees = license.PaidFees,
                    IsActive = license.IsActive,
                    IssueReason = (byte)license.IssueReason,
                    CreatedByUserID = license.CreatedByUserID

                };
                return Ok("License created successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while creating the license.");
            }
        }

        [HttpPut("Update/{id}")]
        public ActionResult<LicenseDTO> Edit(int id, [FromBody] LicenseDTO licenseDto)
        {
            if (id <= 0 || licenseDto == null || id != licenseDto.LicenseID)
            {
                return BadRequest("Invalid data supplied.");
            }
            clsLisence Lisence = clsLisence.GetLicenseByID(id);
            if (Lisence == null)
            {
                return NotFound($"License with ID {id} not found.");
            }
            Lisence.ApplicationID = licenseDto.ApplicationID;
            Lisence.DriverID = licenseDto.DriverID;
            Lisence.LicenseClass = licenseDto.LicenseClass;
            Lisence.IssueDate = licenseDto.IssueDate;
            Lisence.ExpirationDate = licenseDto.ExpirationDate;
            Lisence.Notes = licenseDto.Notes;
            Lisence.PaidFees = licenseDto.PaidFees;
            Lisence.IsActive = licenseDto.IsActive;
            Lisence.IssueReason = (clsLisence.enIssueReason)licenseDto.IssueReason;
            Lisence.CreatedByUserID = licenseDto.CreatedByUserID;

            bool isUpdated = Lisence.Save();
            if (isUpdated)
            {
                LicenseDTO dto = new LicenseDTO
                {

                    LicenseID = Lisence.LicenseID,
                    LicenseClass = Lisence.LicenseClass,
                    DriverID = Lisence.DriverID,
                    ApplicationID = Lisence.ApplicationID,
                    IssueDate = Lisence.IssueDate,
                    ExpirationDate = Lisence.ExpirationDate,
                    Notes = Lisence.Notes,
                    PaidFees = Lisence.PaidFees,
                    IsActive = Lisence.IsActive,
                    IssueReason = (byte)Lisence.IssueReason,
                    CreatedByUserID = Lisence.CreatedByUserID



                };
                return Ok(dto);

            }
            else
            {
                return StatusCode(500, "An error occurred while updating the license.");
            }
        }

        public ActionResult<bool> DeactiveLicense(int LicenseID)
        {
            if (LicenseID <= 0)
            {
                return BadRequest("Invalid ID supplied.");
            }
            var result = clsLisence.DeactivateLicense(LicenseID);
            if (!result)
                return NotFound($"License with ID {LicenseID} not found or could not be deactivated.");
            return Ok(result);
        }
    }

}

