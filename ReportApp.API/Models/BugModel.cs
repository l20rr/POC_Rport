using Microsoft.EntityFrameworkCore;
using ReportApp.Shared;

namespace ReportApp.API.Models
{
      //BugModel and database connection
    public class BugModel : IBugModel
    {
        private readonly AppDbContext _appDbContext;

        public BugModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<BugReport> Addbug(BugReport bug)
        {
            var result = await _appDbContext.Bugs.AddAsync(bug);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAll()
        {
            var foundDos = _appDbContext.Bugs.OrderByDescending(d => d.BugReportId).ToList();

            foreach (var bugs in foundDos)
            {
                _appDbContext.Bugs.Remove(bugs);
            }

            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<BugWithUserDetails> GetAllBugs()
        {
            var BugWithUserDetails = _appDbContext.Bugs
              .Include(f => f.User) 
              .Select(f => new BugWithUserDetails
              {
                  BugReportId = f.BugReportId,
                  UserId = f.UserId,
                  Timestamp = f.Timestamp,
                  Description = f.Description,
                  AttachmentId = f.AttachmentId,
                  UserName = f.User.UserName,
                  UserEmail = f.User.Email
              })
              .ToList();

            return BugWithUserDetails;
        }

        BugWithUserDetails IBugModel.GetBugById(int bugId)
        {

            var feedbackWithUser = _appDbContext.Bugs
            .Include(f => f.User) 
            .Where(f => f.BugReportId == bugId)
            .Select(f => new BugWithUserDetails
            {
               BugReportId = f.BugReportId,
               UserId = f.UserId,
               Timestamp = f.Timestamp,
               Description = f.Description,
               AttachmentId = f.AttachmentId,
               UserName = f.User.UserName,
               UserEmail = f.User.Email
             })
             .FirstOrDefault();

            return feedbackWithUser;
        }
    }
    
}
