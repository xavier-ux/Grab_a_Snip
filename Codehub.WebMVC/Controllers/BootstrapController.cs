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
    public class BootstrapController : Controller
    {
        // GET: Bootstrap
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BootstrapService(userId);
            var model = service.GetBootstrap();

            return View(model);
        }
        public ActionResult Index2()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BootstrapService(userId);
            var model = service.GetAllBootstrap();

            return View(model);
        }
        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BootstrapCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateBootstrapService();

            if (service.CreateBootstrap(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var svc = CreateBootstrapService();
            var model = svc.GetBootstrapById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateBootstrapService();
            var detail = service.GetBootstrapById(id);
            var model =
                new BootstrapEdit
                {
                    BootstrapId = detail.BootstrapId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BootstrapEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BootstrapId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateBootstrapService();

            if (service.UpdateBootstrap(model))
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
            var svc = CreateBootstrapService();
            var model = svc.GetBootstrapById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateBootstrapService();

            service.DeleteBootstrap(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private BootstrapService CreateBootstrapService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BootstrapService(userId);
            return service;
        }
    }
}