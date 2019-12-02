using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MailServices.Models;
using MailServices.MgnClasses;

namespace MailServices.Controllers
{
    public class SendMailsController : Controller
    {
        private MailServicesContext db = new MailServicesContext();

        // GET: SendMails
        public async Task<ActionResult> Index()
        {
            return View(await db.sendMails.ToListAsync());
        }

        // GET: SendMails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMail sendMail = await db.sendMails.FindAsync(id);
            if (sendMail == null)
            {
                return HttpNotFound();
            }
            return View(sendMail);
        }

        // GET: SendMails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SendMails/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MailId,From,To,Subject,HtmlContent")] SendMail sendMail)
        {
            var From = sendMail.From;   
            var FromName = "hola";   
            var To = sendMail.To; 
            var Subject = sendMail.Subject;    
            var PlainTextContent = "-----";   
            var HtmlContent = sendMail.HtmlContent;    

            if (ModelState.IsValid)
            {
                SendGridClass.Main(From, FromName, To, Subject, PlainTextContent, HtmlContent);
                db.sendMails.Add(sendMail);
                await db.SaveChangesAsync();
                return RedirectToAction("Details");
            }

            return View(sendMail);
        }

        // GET: SendMails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMail sendMail = await db.sendMails.FindAsync(id);
            if (sendMail == null)
            {
                return HttpNotFound();
            }
            return View(sendMail);
        }

        // POST: SendMails/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MailId,From,To,Subject,HtmlContent")] SendMail sendMail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendMail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sendMail);
        }

        // GET: SendMails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendMail sendMail = await db.sendMails.FindAsync(id);
            if (sendMail == null)
            {
                return HttpNotFound();
            }
            return View(sendMail);
        }

        // POST: SendMails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SendMail sendMail = await db.sendMails.FindAsync(id);
            db.sendMails.Remove(sendMail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //public async Task<ActionResult> MailDetailsAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SendMail sendMail = await db.sendMails.FindAsync(id);
        //    if (sendMail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sendMail);
        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
