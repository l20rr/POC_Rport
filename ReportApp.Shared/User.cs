using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp.Shared
{
	public class User
	{
		public int UserId { get; set; }


		[Required]
		[StringLength(50, ErrorMessage = "The name length can't exceed 50 characters.")]
		public string UserName { get; set; }


		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
