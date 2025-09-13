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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UsersController : ControllerBase
    {
        [HttpGet("GetByID/{userID}")]
        public ActionResult<UserDTO> FindByID(int userID)
        {
            if (userID <= 0)
                return BadRequest("Invalid UserID");
            var user = business.clsUser.FindByUserID(userID);
            if (user == null)
                return NotFound();
            return Ok(user);

        }

        [HttpGet("GetByPersonID/{personID}")]
        public ActionResult<UserDTO> FindByPersonID(int personID)
        {
            if (personID <= 0)
                return BadRequest("Invalid UserID");
            var user = business.clsUser.FindByPersonID(personID);
            if (user == null)
                return NotFound();
            return Ok(user);

        }

        [HttpGet("{password}/FindByUsernameAndPassword/{username}")]
        public ActionResult<UserDTO> FindByUsernameAndPassword(string username, string password)
        {
            if (username == null || password == null)
                return BadRequest("Invalid UserID");
            var user = business.clsUser.FindByUsernameAndPassword(username, password);
            if (user == null)
                return NotFound();
            return Ok(user);

        }

        [HttpGet("isUserExist/{userID}")]
        public ActionResult<bool> isUserExist(int userID)
        {
            if (userID <= 0)
                return BadRequest($"Invalid User ID: {userID}");

            bool isExist = clsUser.isUserExist(userID);

            if (!isExist)
                return NotFound($"User with ID {userID} not found.");

            return Ok(true);
        }

        [HttpGet("IsExist/{username}")]
        public ActionResult<bool> IsUserExistByusername(string username)

        {
            if (username == null)
            {
                return BadRequest($"invalid username{username}");
            }
            var isExist = clsUser.isUserExist(username);

            return Ok(isExist);
        }

        [HttpGet("GetAllUsers")]
        public ActionResult<List<UserDetailsDTO>> GetAllUsers()
        {
            var users = clsUser.GetAllUsers();
            return Ok(users);
        }

        [HttpDelete("Delete/{userID}")]
        public ActionResult Delete(int userID)
        {
            if (userID <= 0)
                return BadRequest("Invalid UserID");

            var result = clsUser.DeleteUser(userID);
            if (result)
                return Ok();
            else
                return StatusCode(500, "An error occurred while deleting the user.");
        }

        [HttpPost("CreateUser")]
        public ActionResult<UserDTO> CreateUser([FromBody] UserDTO userDto)
        {

            if (userDto == null)
                return BadRequest("User data is null.");

            var user = new clsUser
            {
                PersonID = userDto.PersonID,
                UserName = userDto.UserName,
                Password = userDto.Password,
                IsActive = userDto.IsActive
            };

            if (user.Save())
            {
                userDto.UserID = user.UserID;
                return CreatedAtAction(nameof(FindByID), new { userID = user.UserID }, userDto);
            }
            else
            {
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }
      
        [HttpPut("UpdateUser/{userID}")]
        public ActionResult<UserDTO> Edit(int userID, [FromBody] UserDTO userDto)
        {
            if (userDto == null || userID != userDto.UserID)
                return BadRequest("User data is invalid.");
            var user = clsUser.FindByUserID(userID);

            user.PersonID = userDto.PersonID;
            user.UserName = userDto.UserName;
            user.Password = userDto.Password;
            user.IsActive = userDto.IsActive;
            if (user.Save())
            {
                var userDTO = new UserDTO
                {
                    UserID = user.UserID,
                    PersonID = user.PersonID,
                    UserName = user.UserName,
                    Password = user.Password,
                    IsActive = user.IsActive
                };
                return Ok(userDTO);
            }
            else
            {
                return StatusCode(500, "An error occurred while updating the user.");
            }

        }

        [HttpGet("IsUserExistByPersonID/{personID}")]
        public ActionResult<bool> IsUserExistByPersonID(int personID)

        {
            if (personID <=0)
            {
                return BadRequest($"invalid username{personID}");
            }
            var isExist = clsUser.isUserExistForPersonID(personID); 

            return Ok(isExist);
        }

    }
}
