using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dvld.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UsersController : ControllerBase
    {
        [HttpGet("GetByID/{userID}")]
        public ActionResult<UserDTO> FindByID(int userID)
        {
            if(userID <= 0)
                return BadRequest("Invalid UserID");
            var user = business.clsUser.FindByUserID(userID);
            if(user == null)
                return NotFound();
            return Ok(user);

        }


    }
}
