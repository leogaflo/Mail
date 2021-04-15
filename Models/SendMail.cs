using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MailServices.Models
{
    public class SendMail
    {
        [Key]
        public int MailId { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "De")]
        public string From { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Para")]
        //public string Contacts { get; set; }

        public string To { get; set; }


        [Required]
        [Display(Name = "Asuntos")]
        public string Subject { get; set; }


        [Required]
        [Display(Name = "Mensaje")]
        public string HtmlContent { get; set; }

        //[ForeignKey("User")]
        public int MailUserID { get; set; }

        public User User { get; set; }

    }
}