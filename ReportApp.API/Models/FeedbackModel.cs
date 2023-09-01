using Microsoft.EntityFrameworkCore;
using ReportApp.Shared;

namespace ReportApp.API.Models
{
    //FeedbackModel and database connection
    public class FeedbackModel: IFeedbackModel
    {
        private readonly AppDbContext _appDbContext;

        public FeedbackModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Feedback> AddFeedback(Feedback feedback)
        {
            var result = await _appDbContext.Feedbacks.AddAsync(feedback);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAll()
        {
            var foundDos = _appDbContext.Feedbacks.OrderByDescending(d => d.FeedbackId).ToList();

            foreach (var Feedbacks in foundDos)
            {
                _appDbContext.Feedbacks.Remove(Feedbacks);
            }

            await _appDbContext.SaveChangesAsync();

        }

        public IEnumerable<FeedbackWithUserDetails> GetAllFeedbacks()
        {
            var feedbacksWithUsers = _appDbContext.Feedbacks
         .Include(f => f.User) 
         .Select(f => new FeedbackWithUserDetails
         {
             FeedbackId = f.FeedbackId,
             UserId = f.UserId,
             Timestamp = f.Timestamp,
             Ranking = f.Ranking,
             AttachmentId = f.AttachmentId,
             Question1 =f.Question1,
			 Question2 = (int)f.Question2,
			 Question3 = (bool)f.Question3,
			 Comments = f.Comments,
             UserName = f.User.UserName,
             UserEmail = f.User.Email
         })
         .ToList();

            return feedbacksWithUsers;
        }

        public FeedbackWithUserDetails GetFeedbackById(int feedbackId)
        {
            var feedbackWithUser = _appDbContext.Feedbacks
       .Include(f => f.User) 
       .Where(f => f.FeedbackId == feedbackId)
       .Select(f => new FeedbackWithUserDetails
       {
           FeedbackId = f.FeedbackId,
           UserId = f.UserId,
           Timestamp = f.Timestamp,
           Ranking = f.Ranking,
           AttachmentId = f.AttachmentId,
           Question1 = f.Question1,
		   Question2 = (int)f.Question2,
		   Question3 = (bool)f.Question3,
		   Comments = f.Comments,
           UserName = f.User.UserName,
           UserEmail = f.User.Email
       })
       .FirstOrDefault();

            return feedbackWithUser;
        }
    }
}
