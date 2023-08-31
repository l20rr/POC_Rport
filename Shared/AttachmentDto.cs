using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp.Shared
{
    public class AttachmentDto
    {


        public int? FeedbackId { get; set; }
        public int? BugReportId { get; set; }
        public string FileName { get; set; }
        public string Base64data { get; set; }
        public string ContentType { get; set; }

    }
}
