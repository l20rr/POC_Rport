using ReportApp.Shared;

namespace ReportApp.API.Models
{
    public interface IAttachment
    {

        IEnumerable<Attachments> GetAllAttachments();
        Attachments GetAttachmentById(int attachmentId);
        Task<Attachments> AddAttachments(Attachments attachments);
    }
}
