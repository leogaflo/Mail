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
    public class UsersManagerController : Controller
    {
        private MailServicesContext db = new MailServicesContext();
        // GET: UsersManager
        public async Task<ActionResult> Index()
        {
            
            return View(await db.Users.ToListAsync());
        }

        // GET: UsersManager/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: UsersManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersManager/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,Name,LastName,Birthday,Email,EmailPassword,UserPassword,Provider,Admin")] User user)
        {
            var From = "Noreply@gmail.com";
            var FromName = "Calico Mail Services";
            var To = user.Email;
            var Subject = "Registracion Calico Web Mail";
            var PlainTextContent = "Correo de Biembenida";
            var HtmlContent = "<html lang="+"en"+"><head><meta charset="+"UTF-8"+"></head><body><div style="+"margin: auto; padding: 100px;"+"><p>Gracias Por registrarte en nuesta web</p><h4><strong>Nombre :</strong></h4><h5> "+user.Name+" </h5><h4><strong>Apellido :</strong></h4><h5> "+user.LastName+" </h5><h4><strong>Usuario :</strong></h4><h5> "+user.Email+" </h5><h4><strong>Contraseña :</strong></h4><h5> "+user.UserPassword+"</h5></div></body></html>";

            if (ModelState.IsValid)
            {
                //Esta linea comprueba si solo se intodujo uno de los proveedores acetables 
                if(user.Provider.Contains("gmail") || user.Provider.Contains("outlook")|| user.Provider.Contains("aol") || user.Provider.Contains("yahoo"))
                {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                SendGridClass.Main(From, FromName, To, Subject, PlainTextContent, HtmlContent);
                return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }

            return View(user);
        }

        // GET: UsersManager/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: UsersManager/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,Name,LastName,Birthday,Email,EmailPassword,UserPassword,Provider,Admin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: UsersManager/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: UsersManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
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
