using ReportApp.Services;
using Microsoft.AspNetCore.Components;
using ReportApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ReportApp.Components
{
	public partial class AddBugReport
	{


		private BugReport BugReport { get; set; } = new BugReport();
		private User User { get; set; } = new User();

		[Inject]
		public IBugReportDataService BugReportDataService { get; set; }

		[Inject]
		public IUserDataService UserDataService { get; set; }
		public bool ShowReportForm { get; set; } = false;

		[Parameter]
		public EventCallback<bool> CloseEventCallback { get; set; }

		private bool isInitialized = false;
		private bool isShowing = false;

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync(); // Chame a implementação base primeiro
			isInitialized = true;
		}



		public async Task ShowAsync()
		{
			if (!isInitialized)
			{
				return;
			}

			ResetReportForm();
			isShowing = true;
			StateHasChanged();

			await Task.Delay(10); // Pequeno atraso para permitir que a renderização seja atualizada

			ShowReportForm = isShowing; // Atualize ShowReportForm após a renderização ser atualizada
			StateHasChanged();
		}

		public void Close()
		{
			ShowReportForm = false;
			StateHasChanged();
		}

		private void ResetReportForm()
		{
			BugReport = new BugReport { UserId = 1, Timestamp = DateTime.Now, Description = "", AttachmentId = 1 };
		}

		private async Task AddBug(int userId)
		{

			if (!string.IsNullOrWhiteSpace(BugReport.Description))
			{

				BugReport.UserId = userId;


				var response = await BugReportDataService.AddBugReport(BugReport);

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
			var response =  await UserDataService.AddUser(User);
			await AddBug(response.UserId);
			ShowReportForm = false;

			await CloseEventCallback.InvokeAsync(true);
			StateHasChanged();
		}
	}
}