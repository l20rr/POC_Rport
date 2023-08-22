using Microsoft.AspNetCore.Mvc;
using ReportApp.API.Models;
using ReportApp.Shared;

namespace ReportApp.API.Controllers
{
    [Route("api/bugs")]
    [ApiController]

    public class BugController : Controller
    {
        private readonly IBugModel _bugModel;

        public BugController(IBugModel bugModel)
        {
            _bugModel = bugModel;
        }
        [HttpGet]
        public IActionResult GetAllBugs()
        {
            try
            {
                var bugWithUserDetails = _bugModel.GetAllBugs();
                return Ok(bugWithUserDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching bugs: {ex.Message}");
            }
        }

        [HttpGet("{bugId}")]
        public IActionResult GetBugById(int bugId)
        {
            try
            {
                var bugWithUserDetails = _bugModel.GetBugById(bugId);

                if (bugWithUserDetails != null)
                {
                    return Ok(bugWithUserDetails);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the bug: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBugs([FromBody] BugReport bug)
        {
            try
            {
                var newBug= await _bugModel.Addbug(bug);
                return CreatedAtAction(nameof(GetBugById), new { bugId = newBug.BugReportId }, newBug);
            }
            catch (Exception ex)
            {
                // Retornar uma resposta BadRequest com os detalhes do erro interno.
                return BadRequest(new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = $"An error occurred while adding the bug: {ex.Message}",
                    Status = 500
                });
            }
        }
    }
}
