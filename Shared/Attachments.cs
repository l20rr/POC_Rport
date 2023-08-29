using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp.Shared
{
	public class Attachments
	{
        public int? AttachmentId { get; set; }


        public int? BugReportId { get; set; }


        public int? FeedbackId { get; set; }


        public string? FileName { get; set; }


        public string? FilePath { get; set; }

        public string? Base64data { get; set; }

        public string? ContentType { get; set; }

        public BugReport? BugReport { get; set; }

        public Feedback? Feedback { get; set; }
    }
}
