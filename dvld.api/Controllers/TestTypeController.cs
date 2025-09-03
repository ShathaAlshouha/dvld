using DTOs;
using dvld.business;
using dvld.data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public class TestTypeController : ControllerBase
    {

        [HttpGet("GetAllTestTypes")]
        public ActionResult<IEnumerable<testTypeDTO>> GetAll()
        {

            List<testTypeDTO> list = new List<testTypeDTO>();
            list = clsTestTypeData.GetAllTestTypes();
            return Ok(list);
        }
        [HttpGet("GetTestTypeByID/{id}")]
        public ActionResult<testTypeDTO> GetByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Faild ID");
            }
            testTypeDTO testTypeDTO = new testTypeDTO();
            bool isFound = clsTestTypeData.GetTestTypeInfoByID(id, ref testTypeDTO);
            if (!isFound)
            {
                return NotFound("Test Type Not Found");
            }
            testTypeDTO = new testTypeDTO
            {
                TestTypeID = id,
                TestTypeTitle = testTypeDTO.TestTypeTitle,
                Description = testTypeDTO.Description,
                TestFees = testTypeDTO.TestFees
            };
            return Ok(testTypeDTO);
        }


        [HttpPost("CreateTestType")]
        public ActionResult<testTypeDTO> ActionResult([FromBody] testTypeDTO newTestType)
        {
            if (newTestType == null || string.IsNullOrEmpty(newTestType.TestTypeTitle) || newTestType.TestFees <= 0)
            {
                return BadRequest("Invalid Test Type Data");
            }

            clsTestType testType = new clsTestType();
            testType.Title = newTestType.TestTypeTitle;
            testType.Description = newTestType.Description;
            testType.Fees = newTestType.TestFees;

            bool isAdded = testType.Save();
            if (!isAdded)
            {
                return BadRequest("Failed to create Test Type");
            }

            newTestType.TestTypeID = (int)testType.ID;

            return CreatedAtAction(nameof(GetByID), new { id = newTestType.TestTypeID }, newTestType);
        }

     
    }
}
