using ReportApp.Shared;
using System.Threading.Tasks;



namespace ReportApp.Services
{
	public interface IBugReportDataService
	{
		Task<IEnumerable<BugReport>> GetAllBugReports();
		Task<BugReport> GetBugReportDetails(int bugReportId);
		Task<BugReport> AddBugReport(BugReport bugReport);
	}
}
