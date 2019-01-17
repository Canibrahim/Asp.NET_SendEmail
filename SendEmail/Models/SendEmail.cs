using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SendEmail.Models
{
    public class SendEmail
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$", ErrorMessage = "Invalid email")]
        public string to { get; set; }

        [Required]
        public string subject { get; set; }

        [Required]
        public string body { get; set; }

        public HttpPostedFileBase[] file { get; set; }
    }
}