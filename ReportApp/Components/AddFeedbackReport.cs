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


		[Inject]
		public IFeedbackDataService FeedbackDataService { get; set; }
		[Inject]
		public IUserDataService UserDataService { get; set; }

		public bool ShowReportForm { get; set; }


		[Parameter]
		public EventCallback<bool> CloseEventCallback { get; set; }

		private int selectedRating = -1;

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
			Feedback = new Feedback();
		}
		private async Task Addfeed(int userId)
		{

			if (!string.IsNullOrWhiteSpace(Feedback.Comments))
			{

				Feedback.UserId = userId;


				var response = await FeedbackDataService.AddFeedback(Feedback);

				if (response != null)
				{
					Console.WriteLine("Sucesso: ");
				}
				else
				{
					Console.WriteLine("Erro: Ocorreu um problema ao criar a carteira.");
				}
			}
			else
			{
				Console.WriteLine("Erro: O nome da carteira não pode ser vazio.");
			}
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

