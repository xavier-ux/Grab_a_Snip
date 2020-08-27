using Codehub.Models;
using Codehub.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Codehub.WebMVC.Controllers
{
    [Authorize]
    public class CssController : Controller
    {
        // GET: Css
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CssService(userId);
            var model = service.GetCss();

            return View(model);
        }
        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CssCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCssService();

            if (service.CreateCss(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateCssService();
            var model = svc.GetCssById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateCssService();
            var detail = service.GetCssById(id);
            var model =
                new CssEdit
                {
                    CssId = detail.CssId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CssEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CssId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCssService();

            if (service.UpdateCss(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCssService();
            var model = svc.GetCssById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCssService();

            service.DeleteCss(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private CssService CreateCssService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CssService(userId);
            return service;
        }
    }
}
