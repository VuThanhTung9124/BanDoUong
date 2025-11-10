using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Admin.Models;

namespace Admin.Controllers
{
    public class AccountsController : Controller
    {
        private AccountDb db = new AccountDb();

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Accounts/Details/5



        // GET: Accounts/Delete/5
        [HttpGet]
        public ActionResult Delete()
        {
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Account account = db.Accounts.Find(id);
        //    if (account == null)
        //    {
        //        return HttpNotFound();
        //    }
            return View();
        }

        // POST: Accounts/Delete/5
        [HttpPost]
      
        public ActionResult Delete(string id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
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
