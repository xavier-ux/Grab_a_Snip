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
    public class NewCssCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NewCssCodes
        public ActionResult Index()
        {
            return View(db.CssCodes.ToList());
        }

        // GET: NewCssCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CssCode cssCode = db.CssCodes.Find(id);
            if (cssCode == null)
            {
                return HttpNotFound();
            }
            return View(cssCode);
        }

        // GET: NewCssCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewCssCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CssId,OwnerId,Title,Content,CreatedUtc,ModifiedUtc")] CssCode cssCode)
        {
            if (ModelState.IsValid)
            {
                db.CssCodes.Add(cssCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cssCode);
        }

        // GET: NewCssCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CssCode cssCode = db.CssCodes.Find(id);
            if (cssCode == null)
            {
                return HttpNotFound();
            }
            return View(cssCode);
        }

        // POST: NewCssCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CssId,OwnerId,Title,Content,CreatedUtc,ModifiedUtc")] CssCode cssCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cssCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cssCode);
        }

        // GET: NewCssCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CssCode cssCode = db.CssCodes.Find(id);
            if (cssCode == null)
            {
                return HttpNotFound();
            }
            return View(cssCode);
        }

        // POST: NewCssCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CssCode cssCode = db.CssCodes.Find(id);
            db.CssCodes.Remove(cssCode);
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
