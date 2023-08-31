using ReportApp.Services;
using Microsoft.AspNetCore.Components;
using ReportApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;

namespace ReportApp.Components
{
    public partial class AddBugReport
    {
        private BugReport BugReport { get; set; } = new BugReport();
        private User User { get; set; } = new User();





        [Inject]
        public IBugReportDataService BugReportDataService { get; set; }

        [Inject]
        public IAttachmentService AttachmentService { get; set; }
        [Inject]
        public IUserDataService UserDataService { get; set; }
        public bool ShowReportForm { get; set; } = false;


        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        private bool isInitialized = false;
        private bool isShowing = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
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

            await Task.Delay(10);

            ShowReportForm = isShowing;
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
                    Console.WriteLine("Erro");
                }
            }
            else
            {
                Console.WriteLine("Erro.");
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
            await AddBug(response.UserId);
            await Upload();
            ShowReportForm = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}