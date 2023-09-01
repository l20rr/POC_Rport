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
        //Dependencies
        private BugReport BugReport { get; set; } = new BugReport();
        private User User { get; set; } = new User();

        [Inject]
        public IBugReportDataService BugReportDataService { get; set; }

        [Inject]
        public IAttachmentService AttachmentService { get; set; }
        [Inject]
        public IUserDataService UserDataService { get; set; }
        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        private bool isInitialized = false;
        private bool isShowing = false;
        public bool ShowReportForm { get; set; } = false;

        private bool AreFieldsFilled => !string.IsNullOrWhiteSpace(User.UserName) &&
                        !string.IsNullOrWhiteSpace(User.Email) &&
                        !string.IsNullOrWhiteSpace(BugReport.Description);


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            isInitialized = true;
        }


        //Methods for seeing close and return the component 
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

        //Add bug feedback
        private async Task AddBug(int userId, int lastAttachmentId)
        {
            if (!string.IsNullOrWhiteSpace(BugReport.Description))
            {
                BugReport.UserId = userId;
                BugReport.AttachmentId = lastAttachmentId; 

                var response = await BugReportDataService.AddBugReport(BugReport);

                if (response != null)
                {
                    Console.WriteLine("Sucesso: ");
                }
                else
                {
                    Console.WriteLine("Erro");
                    Console.WriteLine("Feedback Data:");
                    Console.WriteLine($"BugReportId = {BugReport.BugReportId},");
                    Console.WriteLine($"UserId = {BugReport.UserId},");
                    Console.WriteLine($"AttachmentId = {BugReport.AttachmentId},");
                    Console.WriteLine($"Description = {BugReport.Description},");
             
                }
            }
            else
            {
                Console.WriteLine("Erro2.");
            }
        }

        //part for uploading files
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

        public async Task<int> GetLastAttachmentIdAsync()
        {
            var attachments = await AttachmentService.GetAllAttachmentsAsync();

            if (attachments.Any())
            {
                //LINQ for last AttachmentId
                int lastAttachmentId = (int)attachments.Max(a => a.AttachmentId);
                return lastAttachmentId;
            }

            return -1; 
        }


        private async Task HandleValidSubmit()
        {
            var response = await UserDataService.AddUser(User);

            await Upload();
            ShowReportForm = false;

            // Obtenha o último AttachmentId após a conclusão do upload
            int lastAttachmentId = await GetLastAttachmentIdAsync();

            // Exiba o último AttachmentId no console
            Console.WriteLine($"Último AttachmentId inserido: {lastAttachmentId}");

            await AddBug(response.UserId, lastAttachmentId); // Passando lastAttachmentId como argumento
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}