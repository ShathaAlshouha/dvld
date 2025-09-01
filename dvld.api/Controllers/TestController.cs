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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TestController : ControllerBase
    {
        [HttpGet("FindTest/{testID}")]
        public ActionResult <TestDTO> Find(int testID)
        {
            if(testID <= 0)
                return BadRequest("Invalid Test ID");
            clsTest test = clsTest.Find(testID);
            if(test == null)
                return NotFound("Test not found");
            TestDTO testDTO = new TestDTO
            {
                TestAppointmentID = test.TestAppointmentID,
                TestResult = test.TestResult,
                Notes = test.Notes,
                CreatedByUserID = test.CreatedByUserID
            }; 
            return Ok(testDTO);
        }

        [HttpGet("FindByPerson_TestType_LicenseClass/{personId}/{TestType}/{LicenseClass}")]
        public ActionResult<TestDTO> FindByPerson_TestType_LicenseClass (int personId, int TestType, int LicenseClass)
        {
            if (personId <= 0)
                return BadRequest("Invalid Person ID");
            clsTest test = clsTest.FindLastTestPerPersonAndLicenseClass(personId, LicenseClass, (clsTestType.enTestType)TestType);
            if (test == null)
                return NotFound("Test not found");
            TestDTO testDTO = new TestDTO
            {
                TestAppointmentID = test.TestAppointmentID,
                TestResult = test.TestResult,
                Notes = test.Notes,
                CreatedByUserID = test.CreatedByUserID
            };
            return Ok(testDTO);
        }
    }
}
