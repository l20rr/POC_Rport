using Microsoft.AspNetCore.Mvc;
using ReportApp.API.Models;
using ReportApp.Shared;

namespace ReportApp.API.Controllers
{

    [Route("api/users")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserModel _userModel;

        public UserController(IUserModel userModel)
        {
            _userModel = userModel;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                return Ok(_userModel.GetAllUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching users: {ex.Message}");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                return Ok(_userModel.GetUserById(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the user: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet([FromBody] User user)
        {
            try
            {
                var newUser = await _userModel.AddUser(user);
                return CreatedAtAction(nameof(GetUserById), new { userId = newUser.UserId }, newUser);
            }
            catch (Exception ex)
            {
                // Retornar uma resposta BadRequest com os detalhes do erro interno.
                return BadRequest(new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = $"An error occurred while adding the user: {ex.Message}",
                    Status = 500
               
                });
            }
        }
    }
}
