using ReportApp.Shared;

namespace ReportApp.API.Models
{
    //AttachmentModel and database connection
    public class AttachmentModel : IAttachment
    {
        private readonly AppDbContext _appDbContext;

        public AttachmentModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Attachments> AddAttachments(Attachments attachments)
        {
            var result = await _appDbContext.Attachment.AddAsync(attachments);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAll()
        {
            var foundDos = _appDbContext.Attachment.OrderByDescending(d => d.AttachmentId).ToList();

            foreach (var Attachment in foundDos)
            {
                _appDbContext.Attachment.Remove(Attachment);
            }

            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<Attachments> GetAllAttachments()
        {
            return _appDbContext.Attachment;
        }

        public Attachments GetAttachmentById(int attachmentId)
        {
            return _appDbContext.Attachment.FirstOrDefault(c => c.AttachmentId == attachmentId);
        }
    }
}

