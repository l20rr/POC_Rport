using ReportApp.Shared;

namespace ReportApp.Services
{
	public interface IFeedbackDataService
	{
		Task<IEnumerable<FeedbackWithUserDetails>> GetAllFeedbacks();
		Task<Feedback> GetFeedbackDetails(int feedbackId);
		Task<Feedback> AddFeedback(Feedback feedback);
	}
}
