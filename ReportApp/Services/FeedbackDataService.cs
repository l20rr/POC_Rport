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
	public class FeedbackDataService : IFeedbackDataService
	{

		private readonly HttpClient _httpClient;


		public FeedbackDataService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}


		public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
		{
			return await JsonSerializer.DeserializeAsync<IEnumerable<Feedback>>
				(await _httpClient.GetStreamAsync($"api/feedback"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}


		public async Task<Feedback> GetFeedbackDetails(int feedbackId)
		{
			return await JsonSerializer.DeserializeAsync<Feedback>
				(await _httpClient.GetStreamAsync($"api/feedback/{feedbackId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		}


		public async Task<Feedback> AddFeedback(Feedback feedback)
		{
			var feedbackJson = new StringContent(JsonSerializer.Serialize(feedback), Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("api/feedback", feedbackJson);

			if (response.IsSuccessStatusCode)
			{
				return await JsonSerializer.DeserializeAsync<Feedback>(await response.Content.ReadAsStreamAsync());
			}

			return null;
		}
	}
}
