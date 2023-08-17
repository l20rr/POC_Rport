﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp.Shared
{
    public class BugWithUserDetails
    {
        public int BugReportId { get; set; }


        public int UserId { get; set; }


        [StringLength(1500, ErrorMessage = "The description length can't exceed 1500 characters.")]
        public string Description { get; set; }


        public DateTime Timestamp { get; set; }


        public int? AttachmentId { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
