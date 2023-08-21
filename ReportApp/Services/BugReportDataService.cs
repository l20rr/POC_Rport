using ReportApp.Shared;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text;
using System.Net;




namespace ReportApp.Services
{
	public class BugReportDataService : IBugReportDataService
	{
		private readonly HttpClient _httpClient;


		public BugReportDataService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}


		public async Task<IEnumerable<BugReport>> GetAllBugReports()
		{
			return await JsonSerializer.DeserializeAsync<IEnumerable<BugReport>>
				(await _httpClient.GetStreamAsync($"api/bugReport"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}


		public async Task<BugReport> GetBugReportDetails(int bugReportId)
		{
			return await JsonSerializer.DeserializeAsync<BugReport>
				(await _httpClient.GetStreamAsync($"api/bugReport/{bugReportId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}


		public async Task<BugReport> AddBugReport(BugReport bugReport)
		{
			var bugReportJson = new StringContent(JsonSerializer.Serialize(bugReport), Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("api/bugReport", bugReportJson);

			if (response.IsSuccessStatusCode)
			{
				return await JsonSerializer.DeserializeAsync<BugReport>(await response.Content.ReadAsStreamAsync());
			}

			return null;
		}

	}
}
