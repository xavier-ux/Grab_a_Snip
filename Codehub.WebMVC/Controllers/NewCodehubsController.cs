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
    public class NewCodehubsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NewCodehubs
        public ActionResult Index()
        {
            var codehub = db.CodeHubs.Include(ch => ch.CssCodes).Include(ch => ch.BootstrapCodes);
            return View(db.CodeHubs.ToList());
        }
        public ActionResult CreateCodeHub()
        {
            ViewBag.CssId = new SelectList(db.CssCodes, "CssId", "CssTitle");
            ViewBag.BootstrapId = new SelectList(db.BootstrapCodes, "BootstrapId", "BootstrapTitle");
            ViewBag.CssCode = db.CssCodes;
                return View();
        }
        // GET: NewCodehubs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Codehub.Data.CodeHub1 codehub = db.CodeHubs.Find(id);
            if (codehub == null)
            {
                return HttpNotFound();
            }
            return View(codehub);
        }

        // GET: NewCodehubs/Create
        public ActionResult Create()
        {
            ViewBag.CssId = new SelectList(db.CssCodes, "CssId", "CssTitle");
            ViewBag.BootstrapId = new SelectList(db.BootstrapCodes, "BootstrapId", "BootstrapTitle");
            return View();
        }

        // POST: NewCodehubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodehubId,OwnerId,Title,Description,CreatedUtc,ModifiedUtc")] Codehub.Data.CodeHub1 codehub)
        {
            if (ModelState.IsValid)
            {
                db.CodeHubs.Add(codehub);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codehub);
        }

        // GET: NewCodehubs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Codehub.Data.CodeHub1 codehub = db.CodeHubs.Find(id);
            if (codehub == null)
            {
                return HttpNotFound();
            }
            return View(codehub);
        }

        // POST: NewCodehubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodehubId,OwnerId,Title,Description,CreatedUtc,ModifiedUtc")] Codehub.Data.CodeHub1 codehub)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codehub).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codehub);
        }

        // GET: NewCodehubs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Codehub.Data.CodeHub1 codehub = db.CodeHubs.Find(id);
            if (codehub == null)
            {
                return HttpNotFound();
            }
            return View(codehub);
        }

        // POST: NewCodehubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Codehub.Data.CodeHub1 codehub = db.CodeHubs.Find(id);
            db.CodeHubs.Remove(codehub);
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
