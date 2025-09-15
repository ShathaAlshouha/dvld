using DTOs;
using dvld.business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        //public IActionResult<IEnumerable<LicenseDTO>> GetAll()
        //{
        //    var license = clsLisence.
        //    return Ok("LicenseController - GetAll");
        //}
    }

}
