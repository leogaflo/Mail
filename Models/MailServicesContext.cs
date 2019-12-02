using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MailServices.Models
{
    public class MailServicesContext : DbContext
    {
        public MailServicesContext() : base("name=MailServicesContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SendMail> sendMails { get; set; }
    }
}
