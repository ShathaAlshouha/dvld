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
    
    
    }
}
