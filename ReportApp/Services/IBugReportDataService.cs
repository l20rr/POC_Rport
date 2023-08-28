using ReportApp.Shared;
using System.Threading.Tasks;



namespace ReportApp.Services
{
	public interface IBugReportDataService
	{
		Task<IEnumerable<BugWithUserDetails>> GetAllBugReports();
		Task<BugReport> GetBugReportDetails(int bugReportId);
		Task<BugReport> AddBugReport(BugReport bugReport);
	}
}
