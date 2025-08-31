using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using dvld.business;
using dvld.data;
using Microsoft.AspNetCore.Http.HttpResults;
namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class LocalDtivingLisencApplicationController : ControllerBase
    {

        [HttpGet("GetLocalDrivingLicenseApplication/{id}")]
        public ActionResult<LocalDLApp> GetLocalDrivingLicenseApplication(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }
            clsLocalDrivingLicenseApplication Localapplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(id);
            if (Localapplication == null)
            {
                return NotFound("Local Driving Lisence Not Found");
            }
            LocalDLApp dto = new LocalDLApp
            {
                ID = Localapplication.LocalDrivingLicenseApplicationID,
                ApplicationID = Localapplication.ApplicationID,
                LicenseClassID = Localapplication.LicenseClassID
            };


            return Ok(dto);
        }

        [HttpGet("GetAllLocalDrivingLicenseApplications")]
        public ActionResult<IEnumerable<LocalDLApp_View>> GetAllLocalDrivingLicenseApplications()
        {
            var list = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            if (list == null || list.Count == 0)
            {
                return NoContent();
            }

            return Ok(list);
        }

        [HttpGet("Read/{applicationID}")]
        public ActionResult<LocalDLApp> GetByApplicaionID(int applicationID)
        {
            if (applicationID < 1)
            {
                return BadRequest("Invalid ID provided.");
            }
            clsLocalDrivingLicenseApplication Localapplication = clsLocalDrivingLicenseApplication.FindByApplicationID(applicationID);
            if (Localapplication == null)
            {
                return NotFound("Local Driving Lisence Not Found");
            }
            LocalDLApp dto = new LocalDLApp
            {
                ID = Localapplication.LocalDrivingLicenseApplicationID,
                ApplicationID = Localapplication.ApplicationID,
                LicenseClassID = Localapplication.LicenseClassID
            };
            return Ok(dto);
        }

        [HttpPost("create")]
        public ActionResult<LocalDLApp> CreateLocalDrivingLicenseApplication([FromBody] LocalDLApp dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid data.");
            }
            var applicationFound = clsApplication.IsApplicationExist(dto.ApplicationID);

            if (!applicationFound)
                return BadRequest("invalid data: application NOT found");

            var licenseClass = clsLicenseClass.Find(dto.LicenseClassID);
            if (licenseClass == null)
                return BadRequest("invalid data: license class NOT found");


            clsLocalDrivingLicenseApplication newApp = new clsLocalDrivingLicenseApplication
            {
                ApplicationID = dto.ApplicationID,
                LicenseClassID = dto.LicenseClassID,

            };

            newApp.Save();

            LocalDLApp newDTO = new LocalDLApp
            {
                ID = newApp.LocalDrivingLicenseApplicationID,
                ApplicationID = newApp.ApplicationID,
                LicenseClassID = newApp.LicenseClassID
            };
            return Ok(newDTO);
        }

        [HttpPut("UpdateLocalDrivingLicenseApplication/{id}")]
        public ActionResult<LocalDLApp> UpdateLocalDrivingLicenseApplication(int id, [FromBody] LocalDLApp dto)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }

            clsLocalDrivingLicenseApplication LocalApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(id);

            if (LocalApp == null)
            {
                return NotFound($"Local Driving License with ID {id} not found.");
            }

            try
            {
                LocalApp.ApplicationID = dto.ApplicationID;
                LocalApp.LicenseClassID = dto.LicenseClassID;

                bool isSaved = LocalApp.Save();
                if (!isSaved)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update record.");
                }

                var updatedDTO = new LocalDLApp
                {
                    ID = LocalApp.LocalDrivingLicenseApplicationID,
                    ApplicationID = LocalApp.ApplicationID,
                    LicenseClassID = LocalApp.LicenseClassID
                };

                return Ok(updatedDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("DeleteLocalDrivingLicenseApplication/{id}")]
        public ActionResult DeleteLocalDrivingLicenseApplication(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID provided.");
            }

            clsLocalDrivingLicenseApplication LocalApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(id);

            if (LocalApp == null)
            {
                return NotFound($"Local Driving License with ID {id} not found.");
            }

            bool isDeleted = LocalApp.Delete();
            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete record.");
            }

            return Ok($"Local Driving License with ID {id} deleted successfully.");
        }

        [HttpGet("{localAppId}/DoesPassTestType/{testTypeId}")]
        public ActionResult<bool> DoesPassTestType(int localAppId, int testTypeId)
        {
          
            if (!Enum.IsDefined(typeof(clsTestType.enTestType), testTypeId))
                return BadRequest("Invalid TestTypeID");

            bool result = clsLocalDrivingLicenseApplication.DoesPassTestType(localAppId, (clsTestType.enTestType)testTypeId);

            return Ok(result);
        }
    
        [HttpGet("{localAppId}/DoesPassTestTypeInstance/{testTypeId}")]
        public ActionResult<bool> DoesPassTestTypeInstance(int localAppId, int testTypeId)
        {
            if (!Enum.IsDefined(typeof(clsTestType.enTestType), testTypeId))
                return BadRequest("Invalid TestTypeID");

          
            var app = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localAppId);
            if (app == null)
                return NotFound($"Application with ID {localAppId} not found.");

       
            bool result = app.DoesPassTestType((clsTestType.enTestType)testTypeId);

            return Ok(result);
        }

        [HttpGet("{localAppId}/DoesPassPreviousTest/{currentTestTypeId}")]
        public ActionResult<bool> DoesPassPreviousTest(int localAppId, int currentTestTypeId)
        {
            if (!Enum.IsDefined(typeof(clsTestType.enTestType), currentTestTypeId))
                return BadRequest("Invalid TestTypeID");

            var app = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localAppId);
            if (app == null)
                return NotFound($"Application with ID {localAppId} not found.");

            bool result = app.DoesPassPreviousTest((clsTestType.enTestType)currentTestTypeId);

            return Ok(result);
        }

        [HttpGet("{localAppId}/IsAttendTest/{testTypeId}")]

        public ActionResult<bool> IsAttendTest(int localAppId , int testTypeId)
        {

            if (!Enum.IsDefined(typeof(clsTestType.enTestType), testTypeId))
                return BadRequest("Invalid TestTypeID");

            
            clsLocalDrivingLicenseApplication applicaion = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localAppId);
            if (applicaion == null)
                return NotFound($"Application with ID {localAppId} not found."); 

            bool result = applicaion.DoesAttendTestType((clsTestType.enTestType)testTypeId);
            return Ok(result); 
        }

        [HttpGet("{localAppId}/TotalTrialperTests/{testTypeId}")]
        public ActionResult<int> TotalTrialperTests(int localAppId, int testTypeId)
        {
            int totalTrials = 0;
            if (!Enum.IsDefined(typeof(clsTestType.enTestType), testTypeId) || localAppId <= 0)
                return BadRequest("Invalid TestTypeID");

            clsLocalDrivingLicenseApplication applicaion = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localAppId);
            if (applicaion == null)
                return NotFound($"Application with ID {localAppId} not found.");

            totalTrials = applicaion.GetTotalTrialsPerTest((clsTestType.enTestType)testTypeId); 
            return Ok(totalTrials);
        }

        [HttpGet("{localAppId}/IsActiveScheduledTest/{testTypeId}")]
        public ActionResult<bool> IsActiveScheduledTest(int localAppId, int testTypeId)
        {
            if (!Enum.IsDefined(typeof(clsTestType.enTestType), testTypeId))
                return BadRequest("Invalid TestTypeID");

            clsLocalDrivingLicenseApplication applicaion = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localAppId);
            if (applicaion == null)
                return NotFound($"Application with ID {localAppId} not found.");

            bool result = clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(localAppId, testTypeId);
            return Ok(result);
        }
    }
}