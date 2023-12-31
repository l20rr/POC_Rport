﻿using Microsoft.AspNetCore.Mvc;
using ReportApp.API.Models;
using ReportApp.Shared;

namespace ReportApp.API.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedController : Controller
    {
        private readonly IFeedbackModel _feedModel;

        public FeedController(IFeedbackModel feedModel)
        {
            _feedModel = feedModel;
        }
        [HttpGet]
        public IActionResult GetAllFeedbacks()
        {
            try
            {
                var feedbacksWithUserDetails = _feedModel.GetAllFeedbacks();
                return Ok(feedbacksWithUserDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching feedbacks: {ex.Message}");
            }
        }

        [HttpGet("{feedbackId}")]
        public IActionResult GetFeedbackById(int feedbackId)
        {
            try
            {
                var feedbackWithUserDetails = _feedModel.GetFeedbackById(feedbackId);

                if (feedbackWithUserDetails != null)
                {
                    return Ok(feedbackWithUserDetails);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching the feedback: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBugs([FromBody] Feedback feed)
        {
            try
            {
                var newFeed= await _feedModel.AddFeedback(feed);
                return CreatedAtAction(nameof(GetFeedbackById), new { feedbackId = newFeed.FeedbackId }, newFeed);
            }
            catch (Exception ex)
            {
                //  Return a BadRequest response with the details of the internal error.
                return BadRequest(new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = $"An error occurred while adding the bug: {ex.Message}",
                    Status = 500
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _feedModel.DeleteAll();
            return Ok();
        }
    }
}
