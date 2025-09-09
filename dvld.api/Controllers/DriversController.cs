using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using dvld.data;
using dvld.business;
namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class DriversController : ControllerBase
    {
        [HttpGet("GetByPersonID/{PersonID}")]
        public IActionResult GetDriverByPersonID(int PersonID)
        {
            DriverDTO driverDTO = new DriverDTO();
            if (PersonID <= 0)
            {
                return BadRequest("Invalid DriverID");
            }
            bool isFound = clsDriverData.GetDriverInfoByPersonID(PersonID, ref driverDTO);

            if (isFound)
            {
                return Ok(driverDTO);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{DriverID}")]
        public IActionResult GetDriverByID(int DriverID)
        {
            DriverDTO driverDTO = new DriverDTO();
            if (DriverID <= 0)
            {
                return BadRequest("Invalid DriverID");
            }
            bool isFound = clsDriverData.GetDriverInfoByDriverID(DriverID, ref driverDTO);

            if (isFound)
            {
                return Ok(driverDTO);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("AllDrivers")]
        public ActionResult<IEnumerable<Driver_ViewDTO>> GetAllDrivers()
        {
            var drivers = clsDriver.GetAllDrivers();
            if (drivers == null)
            {
                return NotFound();
            }
            return Ok(drivers);
        }

        [HttpPost("AddDriver")]
        public ActionResult<DriverDTO> CreateDriver([FromBody] DriverDTO driverDTO)
        {
            if (driverDTO == null)
            {
                return BadRequest("Driver data is null.");
            }

            clsDriver newDriver = new clsDriver
            {
                PersonID = driverDTO.PersonID,
                CreatedByUserID = driverDTO.CreatedByUserID
            };

            bool isAdded = newDriver.Save();

            if (isAdded)
            {
                driverDTO.DriverID = newDriver.DriverID;
                driverDTO.CreatedDate = newDriver.CreatedDate;
                return CreatedAtAction(nameof(GetDriverByID), new { DriverID = driverDTO.DriverID }, driverDTO);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding the driver.");
            }
        }

        [HttpPut("EditDriver/{DriverID}")]
        public ActionResult<DriverDTO> Edit([FromBody] DriverDTO driverDTO, int DriverID)
        {
            if (driverDTO == null || DriverID <= 0)
            {
                return BadRequest("");
            }
            clsDriver existingDriver = clsDriver.FindByDriverID(DriverID);
            if (existingDriver == null)
            {
                return NotFound();
            }
            existingDriver.PersonID = driverDTO.PersonID;
            existingDriver.CreatedByUserID = driverDTO.CreatedByUserID;
            bool isUpdated = existingDriver.Save();
            if (isUpdated)
            {
                driverDTO.DriverID = existingDriver.DriverID;
                driverDTO.CreatedDate = existingDriver.CreatedDate;
                return Ok(driverDTO);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating the driver.");
            }

        }
    }
}
