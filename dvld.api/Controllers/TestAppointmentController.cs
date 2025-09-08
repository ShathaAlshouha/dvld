using DTOs;
using dvld.business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TestAppointmentController : ControllerBase
    {
        [HttpGet("All")]
        public ActionResult<IEnumerable<TestAppointmentDetailsDTO>> GetAllAppointments()
        {
            List<TestAppointmentDetailsDTO> list = new List<TestAppointmentDetailsDTO>();
            list = clsTestAppointment.GetAllTestAppointments();
            if (list == null || list.Count == 0)
            {
                return NotFound("No appointments found.");
            }
            return Ok(list);
        }

        [HttpGet("ByID/{id}")]
        public ActionResult<TestAppointmentDTO> GetAppointmentByID(int id)
        {

            if (id <= 0)
            {
                return BadRequest("Invalid Appointment ID.");
            }
            clsTestAppointment appointment = clsTestAppointment.Find(id);
            if (appointment == null)
            {

                return NotFound($"Appointment with ID {id} not found.");
            }
            TestAppointmentDTO DTO = new TestAppointmentDTO
            {
                TestAppointmentID = appointment.TestAppointmentID,
                AppointmentDate = appointment.AppointmentDate,
                TestTypeID = (int)appointment.TestTypeID,
                LocalDrivingLicenseApplicationID = appointment.LocalDrivingLicenseApplicationID,
                PaidFees = appointment.PaidFees,
                CreatedByUserID = appointment.CreatedByUserID,
                IsLocked = appointment.IsLocked,
                RetakeTestApplicationID = appointment.RetakeTestApplicationID,
            };
            return Ok(DTO);
        }

        [HttpGet("LastTestAppointment/{LocalApplicationID}/TestType/{testTypeID}")]
        public ActionResult<TestAppointmentDTO> GetLastTestAppointment(int LocalApplicationID, int testTypeID)
        {
            if (LocalApplicationID <= 0 || testTypeID <= 0)
            {
                return BadRequest("Invalid parameters.");
            }
            clsTestAppointment appointment = clsTestAppointment.GetLastTestAppointment(LocalApplicationID, (clsTestType.enTestType)testTypeID);
            if (appointment == null)
            {
                return NotFound("No appointment found.");
            }
            TestAppointmentDTO DTO = new TestAppointmentDTO
            {
                TestAppointmentID = appointment.TestAppointmentID,
                AppointmentDate = appointment.AppointmentDate,
                TestTypeID = (int)appointment.TestTypeID,
                LocalDrivingLicenseApplicationID = appointment.LocalDrivingLicenseApplicationID,
                PaidFees = appointment.PaidFees,
                CreatedByUserID = appointment.CreatedByUserID,
                IsLocked = appointment.IsLocked,
                RetakeTestApplicationID = appointment.RetakeTestApplicationID,
            };
            return Ok(DTO);
        }

        [HttpPost("Create")]
        public ActionResult<TestAppointmentDTO> CreateTestAppointment([FromBody] TestAppointmentDTO appointmentDTO)
        {

            if (appointmentDTO == null)
            {
                return BadRequest("Invalid appointment data.");
            }
            clsTestAppointment newAppointment = new clsTestAppointment
            {
                AppointmentDate = appointmentDTO.AppointmentDate,
                TestTypeID = (clsTestType.enTestType)appointmentDTO.TestTypeID,
                LocalDrivingLicenseApplicationID = appointmentDTO.LocalDrivingLicenseApplicationID,
                PaidFees = appointmentDTO.PaidFees,
                CreatedByUserID = appointmentDTO.CreatedByUserID,
                IsLocked = appointmentDTO.IsLocked,
                RetakeTestApplicationID = appointmentDTO.RetakeTestApplicationID,
            };
            if (!newAppointment.Save())
            {
                return BadRequest("Failed to create appointment.");
            }
            TestAppointmentDTO testAppointmentDTO = new TestAppointmentDTO
            {
                TestAppointmentID = newAppointment.TestAppointmentID,
                AppointmentDate = newAppointment.AppointmentDate,
                TestTypeID = (int)newAppointment.TestTypeID,
                LocalDrivingLicenseApplicationID = newAppointment.LocalDrivingLicenseApplicationID,
                PaidFees = newAppointment.PaidFees,
                CreatedByUserID = newAppointment.CreatedByUserID,
                IsLocked = newAppointment.IsLocked,
                RetakeTestApplicationID = newAppointment.RetakeTestApplicationID,
            };
            return CreatedAtAction(nameof(GetAppointmentByID), new { id = testAppointmentDTO.TestAppointmentID }, testAppointmentDTO);
        }

        [HttpPut ("Update/{id}")]
        public ActionResult<TestAppointmentDTO> UpdateTestAppointment(int id, [FromBody] TestAppointmentDTO appointmentDTO)
        {
            if (id <= 0 || appointmentDTO == null || id != appointmentDTO.TestAppointmentID)
            {
                return BadRequest("Invalid data.");
            }
            clsTestAppointment existingAppointment = clsTestAppointment.Find(id);
            if (existingAppointment == null)
            {
                return NotFound($"Appointment with ID {id} not found.");
            }
            existingAppointment.AppointmentDate = appointmentDTO.AppointmentDate;
            existingAppointment.TestTypeID = (clsTestType.enTestType)appointmentDTO.TestTypeID;
            existingAppointment.LocalDrivingLicenseApplicationID = appointmentDTO.LocalDrivingLicenseApplicationID;
            existingAppointment.PaidFees = appointmentDTO.PaidFees;
            existingAppointment.CreatedByUserID = appointmentDTO.CreatedByUserID;
            existingAppointment.IsLocked = appointmentDTO.IsLocked;
            existingAppointment.RetakeTestApplicationID = appointmentDTO.RetakeTestApplicationID;

            if (!existingAppointment.Save())
            {
                return BadRequest("Failed to update appointment.");
            }
            TestAppointmentDTO updatedDTO = new TestAppointmentDTO
            {
                TestAppointmentID = existingAppointment.TestAppointmentID,
                AppointmentDate = existingAppointment.AppointmentDate,
                TestTypeID = (int)existingAppointment.TestTypeID,
                LocalDrivingLicenseApplicationID = existingAppointment.LocalDrivingLicenseApplicationID,
                PaidFees = existingAppointment.PaidFees,
                CreatedByUserID = existingAppointment.CreatedByUserID,
                IsLocked = existingAppointment.IsLocked,
                RetakeTestApplicationID = existingAppointment.RetakeTestApplicationID,
            };
            return Ok(updatedDTO);
        }
    }
}
