using ReportApp.Shared;

namespace ReportApp.API.Models
{
    public interface IBugModel
    {
        //'BugWithUserDetails' return user data
        IEnumerable<BugWithUserDetails> GetAllBugs();
        BugWithUserDetails GetBugById(int bugId);
        Task<BugReport> Addbug(BugReport bug);

        Task DeleteAll();
    }
}
