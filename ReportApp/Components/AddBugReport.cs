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
		public BugReport BugReport { get; set; } = new BugReport { UserId = 1, Timestamp = DateTime.Now, Description = "", AttachmentId = 1 };


		[Inject]
		public IBugReportDataService BugReportDataService { get; set; }


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
			BugReport = new BugReport { UserId = 1, Timestamp = DateTime.Now, Description = "", AttachmentId = 1 };
		}


		protected async Task HandleValidSubmit()
		{
			await BugReportDataService.AddBugReport(BugReport);
			ShowReportForm = false;

			await CloseEventCallback.InvokeAsync(true);
			StateHasChanged();
		}
	}
}
