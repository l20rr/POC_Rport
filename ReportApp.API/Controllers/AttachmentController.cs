using Microsoft.AspNetCore.Mvc;
using ReportApp.API.Models;
using ReportApp.Shared;

namespace ReportApp.API.Controllers
{
    [Route("api/atts")]
    [ApiController]
    public class AttachmentController : Controller
    {
        private readonly IAttachment _attachmentModel;

        public AttachmentController(IAttachment attachmentModel)
        {
            _attachmentModel = attachmentModel;
        }

        [HttpGet]
        public IActionResult GetAllAttachments()
        {
            try
            {
                return Ok(_attachmentModel.GetAllAttachments());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching GetAllAttachments: {ex.Message}");
            }
        }

        [HttpGet("{attachmentId}")]
        public IActionResult GetUserById(int attachmentId)
        {
            try
            {
                return Ok(_attachmentModel.GetAttachmentById(attachmentId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the GetAllAttachments: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAttachments([FromBody] Attachments attachments)
        {
            try
            {
                var newattAchments = await _attachmentModel.AddAttachments(attachments);
                return CreatedAtAction(nameof(GetUserById), new { attachmentId = newattAchments.AttachmentId }, newattAchments);
            }
            catch (Exception ex)
            {
                // Retornar uma resposta BadRequest com os detalhes do erro interno.
                return BadRequest(new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = $"An error occurred while adding the attachment: {ex.Message}",
                    Status = 500

                });
            }
        }
    }
}
