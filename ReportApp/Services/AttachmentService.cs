using ReportApp.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace ReportApp.Services
{
   
        public class AttachmentService : IAttachmentService
        {
     
        private readonly HttpClient _httpClient;

            public AttachmentService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

        public List<Attachments> SelectedFiles { get; set; } = new List<Attachments>();

        private int? _feedbackId = null;

        private int? _bugReportId = null;
        public async Task<Attachments> AddAttachmentAsync(Attachments attachment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/att", attachment);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Attachments>();
            }
            else
            {
                throw new Exception($"Failed to add attachment. Status code: {response.StatusCode}");
            }
        }

        public async Task<List<Attachments>> GetAllAttachmentsAsync()
            {
                return await _httpClient.GetFromJsonAsync<List<Attachments>>("api/att");
            }

            public async Task<Attachments> GetAttachmentByIdAsync(int attachmentId)
            {
                return await _httpClient.GetFromJsonAsync<Attachments>($"api/att/{attachmentId}");
            }

        public void SetFeedbackId(int feedbackId)
        {
            _feedbackId = feedbackId;
        }
        public async Task UploadFiles()
        {
            if (SelectedFiles.Count == 0)
            {
                return; // No files to upload
            }

            foreach (var file in SelectedFiles)
            {
                if (_feedbackId != null)
                {
                    file.FeedbackId = _feedbackId;
                }
                if (_bugReportId != null)
                {
                    file.BugReportId = _bugReportId;
                }

                await AddAttachmentAsync(file); // Save attachment in the database
            }

            SelectedFiles.Clear();
        }


        public void SetBugId(int bugReportId)
        {
            _bugReportId = bugReportId;
        }
    }
    }


