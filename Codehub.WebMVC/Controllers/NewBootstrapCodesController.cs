using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Codehub.Data;

namespace Codehub.WebMVC.Controllers
{
    public class NewBootstrapCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NewBootstrapCodes
        public ActionResult Index()
        {
            return View(db.BootstrapCodes.ToList());
        }

        // GET: NewBootstrapCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BootstrapCode bootstrapCode = db.BootstrapCodes.Find(id);
            if (bootstrapCode == null)
            {
                return HttpNotFound();
            }
            return View(bootstrapCode);
        }

        // GET: NewBootstrapCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewBootstrapCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BootstrapId,OwnerId,Title,Content,CreatedUtc,ModifiedUtc")] BootstrapCode bootstrapCode)
        {
            if (ModelState.IsValid)
            {
                db.BootstrapCodes.Add(bootstrapCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bootstrapCode);
        }

        // GET: NewBootstrapCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BootstrapCode bootstrapCode = db.BootstrapCodes.Find(id);
            if (bootstrapCode == null)
            {
                return HttpNotFound();
            }
            return View(bootstrapCode);
        }

        // POST: NewBootstrapCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BootstrapId,OwnerId,Title,Content,CreatedUtc,ModifiedUtc")] BootstrapCode bootstrapCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bootstrapCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bootstrapCode);
        }

        // GET: NewBootstrapCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BootstrapCode bootstrapCode = db.BootstrapCodes.Find(id);
            if (bootstrapCode == null)
            {
                return HttpNotFound();
            }
            return View(bootstrapCode);
        }

        // POST: NewBootstrapCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BootstrapCode bootstrapCode = db.BootstrapCodes.Find(id);
            db.BootstrapCodes.Remove(bootstrapCode);
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
