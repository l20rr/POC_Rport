using ReportApp.Shared;

namespace ReportApp.Services
{
    public interface IAttachmentService
    {
        Task<List<Attachments>> GetAllAttachmentsAsync();
        Task<Attachments> GetAttachmentByIdAsync(int attachmentId);
        Task<Attachments> AddAttachmentAsync(Attachments attachment);

        List<Attachments> SelectedFiles { get; set; }
       
    }
}
