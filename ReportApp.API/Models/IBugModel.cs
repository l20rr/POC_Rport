using ReportApp.Shared;

namespace ReportApp.API.Models
{
    public interface IBugModel
    {
        IEnumerable<BugWithUserDetails> GetAllBugs();
        BugWithUserDetails GetBugById(int bugId);
        Task<BugReport> Addbug(BugReport bug);
    }
}
