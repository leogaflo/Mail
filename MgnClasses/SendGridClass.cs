using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
namespace MailServices.MgnClasses
{
    public class SendGridClass
    {
            public static void Main(string from, string fromName, string to, string subject, string plainTextContent, string htmlContent )
            {
                    Execute(from, fromName, to, subject, plainTextContent, htmlContent);
            }

           public static void  Execute(string From ,string FromName, string To, string Subject, string PlainTextContent, string HtmlContent)
            {
                //var apiKey = ""
                var client = new SendGridClient("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");
                var from = new EmailAddress(From, FromName);
                var subject = Subject;
                var to = new EmailAddress(To, "Dear");
                var plainTextContent = PlainTextContent;
                var htmlContent = "<strong>"+HtmlContent+"</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response =  client.SendEmailAsync(msg);
            }
        }
    

}