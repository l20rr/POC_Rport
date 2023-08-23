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


        private BugReport BugReport { get; set; } = new BugReport { UserId = 1, Timestamp = DateTime.Now, Description = "", AttachmentId = 1 };

        private User User { get; set; } = new User();

        [Inject]
        public IBugReportDataService BugReportDataService { get; set; }

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

        protected async Task HandleValidSubmit()
        {
            await BugReportDataService.AddBugReport(BugReport);
            ShowReportForm = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
