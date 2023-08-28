using ReportApp.Services;
using Microsoft.AspNetCore.Components;
using ReportApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ReportApp.Components
{
	public partial class AddFeedbackReport
	{

		public Feedback Feedback { get; set; } = new Feedback();
		private User User { get; set; } = new User();
		private bool AreFieldsFilled => !string.IsNullOrWhiteSpace(User.UserName) &&
								!string.IsNullOrWhiteSpace(User.Email) &&
								!string.IsNullOrWhiteSpace(Feedback.Comments)&&
								  selectedRating > 0;


		[Inject]
		public IFeedbackDataService FeedbackDataService { get; set; }
		[Inject]
		public IUserDataService UserDataService { get; set; }

		public bool ShowReportForm { get; set; }
		public bool Questions { get; set; } = false;

		private string recommendationInput = string.Empty;
		[Parameter]
		public EventCallback<bool> CloseEventCallback { get; set; }

		private int selectedRating;

		private void UpdateRating(int rating)
		{
			selectedRating = rating; // Store the selected rating in the property
			Console.WriteLine($"Avaliação selecionada: {rating}");
		}
		public void Show()
		{
			ResetReportForm();
			ShowReportForm = true;
			StateHasChanged();
		}


		public void Close()
		{
			ShowReportForm = false;
			StateHasChanged();
		}


		private void ResetReportForm()
		{
			Feedback = new Feedback();
		}

		private void nextcomponent()
		{
			if (AreFieldsFilled)
			{
				Questions = true;
				ShowReportForm = false;
			}
		}
		private async Task Addfeed(int userId)
		{

			if (!string.IsNullOrWhiteSpace(Feedback.Comments ))
			{

				Feedback.UserId = userId;
				Feedback.Ranking = selectedRating;

				bool.TryParse(recommendationInput.Trim().ToLower(), out bool recommendationValue);
				Feedback.Question3 = recommendationValue;


				var response = await FeedbackDataService.AddFeedback(Feedback);

				Console.WriteLine("Feedback Data:");
					Console.WriteLine($"FeedbackId = {Feedback.FeedbackId},");
					Console.WriteLine($"UserId = {Feedback.UserId},");
					Console.WriteLine($"Timestamp = {Feedback.Timestamp},");
					Console.WriteLine($"Ranking = {Feedback.Ranking},");
					Console.WriteLine($"Comments = \"{Feedback.Comments}\",");
					Console.WriteLine($"AttachmentId = {Feedback.AttachmentId},");
					Console.WriteLine($"Question1 = \"{Feedback.Question1}\",");
					Console.WriteLine($"Question2 = {Feedback.Question2},");
					Console.WriteLine($"Question3 = {Feedback.Question3}");

				if (response != null)
				{
					Console.WriteLine("Sucesso: ");
				}
				else
				{
					Console.WriteLine("Feedback Data:");
					Console.WriteLine($"FeedbackId = {Feedback.FeedbackId},");
					Console.WriteLine($"UserId = {Feedback.UserId},");
					Console.WriteLine($"Timestamp = {Feedback.Timestamp},");
					Console.WriteLine($"Ranking = {Feedback.Ranking},");
					Console.WriteLine($"Comments = \"{Feedback.Comments}\",");
					Console.WriteLine($"AttachmentId = {Feedback.AttachmentId},");
					Console.WriteLine($"Question1 = \"{Feedback.Question1}\",");
					Console.WriteLine($"Question2 = {Feedback.Question2},");
					Console.WriteLine($"Question3 = {Feedback.Question3}");
				}
			}
			else
			{
				Console.WriteLine("Erro: O nome não pode ser vazio.");
			}
		}
		private void PreviousComponent()
		{
			Questions = false;
			ShowReportForm = true;
		}

		private async Task HandleValidSubmit()
		{
			var response = await UserDataService.AddUser(User);
			await Addfeed(response.UserId);
			ShowReportForm = false;

			await CloseEventCallback.InvokeAsync(true);
			StateHasChanged();
		}
	}
}

