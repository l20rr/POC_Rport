﻿using ReportApp.Services;
using Microsoft.AspNetCore.Components;
using ReportApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

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

        [Inject]
        public IAttachmentService AttachmentService { get; set; }
        public bool ShowReportForm { get; set; }
		public bool Questions { get; set; } = false;

		public bool ShowformP { get; set; } = true;

		private string recommendationInput = string.Empty;
		[Parameter]
		public EventCallback<bool> CloseEventCallback { get; set; }

		private int selectedRating;

		private void UpdateRating(int rating)
		{
			selectedRating = rating; 
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
			Feedback = new Feedback { UserId = 1, Timestamp = DateTime.Now, Comments = "", AttachmentId = 1 };
		}

		private void Nextcomponent()
		{
			if (AreFieldsFilled)
			{
				Questions = true;
				ShowformP = false;
			}
		}

		private void PreviousComponent()
		{
			Questions = false;
			ShowReportForm = true;
			ShowformP = true;
		}
		private async Task Addfeed(int userId)
		{

			if (!string.IsNullOrWhiteSpace(Feedback.Comments ))
			{

				Feedback.UserId = userId;
				Feedback.Ranking = selectedRating;

                var response = await FeedbackDataService.AddFeedback(Feedback);

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

	
	
		List<Attachments> filesBase64 = new List<Attachments>();
	
		bool isDisable = false;

		async Task OnChange(InputFileChangeEventArgs e)
		{
			var files = e.GetMultipleFiles();

			foreach (var file in files)
			{
				var buf = new byte[file.Size];

				using (var stream = file.OpenReadStream())
				{
					await stream.ReadAsync(buf);
				}

				filesBase64.Add(new Attachments { Base64data = Convert.ToBase64String(buf), ContentType = file.ContentType, FileName = file.Name });
			}

			
		}

		async Task Upload()
		{
			isDisable = true;

			using (var msg = await Http.PostAsJsonAsync<List<Attachments>>("https://localhost:7046/api/att", filesBase64, System.Threading.CancellationToken.None))
			{
				isDisable = false;

				if (msg.IsSuccessStatusCode)
				{
					

					filesBase64.Clear();
				}
			}
		}


        private async Task HandleValidSubmit()
		{
			var response = await UserDataService.AddUser(User);
			 await Addfeed(response.UserId);
			await Upload();
			ShowReportForm = false;

			await CloseEventCallback.InvokeAsync(true);
			StateHasChanged();
		}
	}
}