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

namespace MailServices.Controllers
{
    public class UserContactsController : Controller
    {
        private MailServicesContext db = new MailServicesContext();

        // GET: UserContacts
        public async Task<ActionResult> Index()
        {
            var userContacts = db.UserContacts.Include(u => u.User);
            return View(await userContacts.ToListAsync());
        }

        // GET: UserContacts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserContacts userContacts = await db.UserContacts.FindAsync(id);
            if (userContacts == null)
            {
                return HttpNotFound();
            }
            return View(userContacts);
        }

        // GET: UserContacts/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name");
            return View();
        }

        // POST: UserContacts/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdContacts,Nombre,Apellido,FechaNac,Email,Sexo,UserID")] UserContacts userContacts)
        {
            if (ModelState.IsValid)
            {
                db.UserContacts.Add(userContacts);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", userContacts.UserID);
            return View(userContacts);
        }

        // GET: UserContacts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserContacts userContacts = await db.UserContacts.FindAsync(id);
            if (userContacts == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", userContacts.UserID);
            return View(userContacts);
        }

        // POST: UserContacts/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdContacts,Nombre,Apellido,FechaNac,Email,Sexo,UserID")] UserContacts userContacts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userContacts).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Name", userContacts.UserID);
            return View(userContacts);
        }

        // GET: UserContacts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserContacts userContacts = await db.UserContacts.FindAsync(id);
            if (userContacts == null)
            {
                return HttpNotFound();
            }
            return View(userContacts);
        }

        // POST: UserContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserContacts userContacts = await db.UserContacts.FindAsync(id);
            db.UserContacts.Remove(userContacts);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
