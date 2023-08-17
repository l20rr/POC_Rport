using ReportApp.Shared;

namespace ReportApp.API.Models
{
    public interface IFeedbackModel
    {
        IEnumerable<FeedbackWithUserDetails> GetAllFeedbacks();
        FeedbackWithUserDetails GetFeedbackById(int feedbackId);
        Task<Feedback> AddFeedback(Feedback feedback);
    }
}
