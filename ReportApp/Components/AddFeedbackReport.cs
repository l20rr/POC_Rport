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

		public Feedback Feedback { get; set; } = new Feedback { UserId = 1, Timestamp = DateTime.Now, Ranking = 0, Comments = "", AttachmentId = 1 , Question1 = "", Question2 = "", Question3 = "" };


		[Inject]
		public IFeedbackDataService FeedbackDataService { get; set; }


		public bool ShowReportForm { get; set; }


		[Parameter]
		public EventCallback<bool> CloseEventCallback { get; set; }


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
			Feedback = new Feedback { UserId = 1, Timestamp = DateTime.Now, Ranking = 0, Comments = "", AttachmentId = 1, Question1 = "", Question2 = "", Question3 = "" };
		}


		protected async Task HandleValidSubmit()
		{
			await FeedbackDataService.AddFeedback(Feedback);
			ShowReportForm = false;

			await CloseEventCallback.InvokeAsync(true);
			StateHasChanged();
		}


	}
}

